using System;
using Core.Extensions;
using Core.Movement.Components;
using Core.Movement.Gravity.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Movement.Gravity.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public sealed class GravitySystem : IFixedSystem
    {
        private const float G = 0.06674f; //6,7 * 10^-11
        private Filter _gravityFilter;
        private Filter _movableFilter;
        private Stash<Position> _positionPool;
        private Stash<Mass> _massPool;
        private Stash<GravityPoint> _gravityPoint;

        public World World { get; set; }

        public void OnAwake()
        {
            _gravityFilter = World.Filter.With<GravityPoint>().With<Position>().Build();
            _movableFilter = World.Filter.With<Position>().With<Velocity>().Without<GravityResistTag>().Build();
            _positionPool = World.GetStash<Position>();
            _massPool = World.GetStash<Mass>();
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
                    World.SendMessage(new ForceRequest { Value = force, EntityId = movableEntity.ID });
                }
            }
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