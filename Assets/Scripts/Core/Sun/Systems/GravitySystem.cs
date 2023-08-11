using Core.Movement.Components;
using Core.Sun.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Sun.Systems
{
    public class GravitySystem : IFixedSystem
    {
        private const float G = 0.06674f; //6,7 * 10^-11
        private Filter _gravityFilter;
        private Filter _movableFilter;
        private Stash<Position> _positionPool;
        private Stash<Mass> _massPool;
        private Stash<ForceRequest> _forceRequestPool;
        private Stash<GravityPoint> _gravityPoint;

        public World World { get; set; }

        public void OnAwake()
        {
            _gravityFilter = World.Filter.With<GravityPoint>().With<Position>();
            _movableFilter = World.Filter.With<Position>().With<Velocity>().Without<GravityResistTag>();
            _positionPool = World.GetStash<Position>();
            _massPool = World.GetStash<Mass>();
            _forceRequestPool = World.GetStash<ForceRequest>();
            _gravityPoint = World.GetStash<GravityPoint>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var gravityPointEntity in _gravityFilter)
            {
                ref var gravityPointPosition = ref _positionPool.Get(gravityPointEntity);
                ref var pointMass = ref _massPool.Get(gravityPointEntity);
                ref var gravityPoint = ref _gravityPoint.Get(gravityPointEntity);

                foreach (var movableEntity in _movableFilter)
                {
                    ref var position = ref _positionPool.Get(movableEntity);
                    ref var mass = ref _massPool.Get(movableEntity);
                    var targetDirection = gravityPointPosition.Value - position.Value;
                    var distance = targetDirection.magnitude;

                    if (distance <= gravityPoint.InnerRadius || distance >= gravityPoint.OuterRadius)
                        continue;

                    var massProduct = pointMass.Value * mass.Value * G;
                    var force = CalculateForce(distance, gravityPoint, massProduct, targetDirection, mass);
                    AddForceToEntity(movableEntity, force);
                }
            }
        }

        private void AddForceToEntity(Entity movableEntity, Vector2 force)
        {
            if (_forceRequestPool.Has(movableEntity) == false)
            {
                _forceRequestPool.Add(movableEntity);
            }

            _forceRequestPool.Get(movableEntity).Value += force;
        }

        private static Vector2 CalculateForce(in float distance, in GravityPoint gravityPoint, in float massProduct,
            in Vector2 targetDirection, in Mass mass)
        {
            var radiusRatio = 1 - Mathf.Clamp01(distance / gravityPoint.OuterRadius);
            var unscaledForceMagnitude = massProduct * radiusRatio / Mathf.Pow(distance, 2);
            var forceMagnitude = G * unscaledForceMagnitude;
            var forceDirection = targetDirection.normalized;
            var force = forceDirection * forceMagnitude / mass.Value;
            return force;
        }

        public void Dispose()
        {
        }
    }
}