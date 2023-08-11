using Core.EnergyLimits.Components;
using Core.Movement.Components;
using Core.Player.Components;
using Core.Sun.Components;
using Scellecs.Morpeh;
using UnityEngine;
using WorldExtensions = Core.Extensions.WorldExtensions;

namespace Core.Sun.Systems
{
    public class SunChargeSystem : ISystem
    {
        private const float MIN_ROTATION_COEFICIENT = 0.1f;
        private Filter _entityFilter;
        private Filter _sunFilter;
        private Stash<Rotation> _rotationPool;
        private Stash<Position> _positionPool;
        private Stash<ChargeContainer> _chargeContainerPool;
        private Stash<GravityPoint> _gravityPointPool;
        private Stash<ChargeRequest> _chargeRequestPool;

        public World World { get; set; }
        
        public void OnAwake()
        {
            _entityFilter = World.Filter.With<Energy>().With<Rotation>().With<Position>();
            _sunFilter = World.Filter.With<GravityPoint>().With<Position>().With<ChargeContainer>();
            _positionPool = World.GetStash<Position>();
            _rotationPool = World.GetStash<Rotation>();
            _chargeContainerPool = World.GetStash<ChargeContainer>();
            _gravityPointPool = World.GetStash<GravityPoint>();
            _chargeRequestPool = World.GetStash<ChargeRequest>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var sunEntity in _sunFilter)
            {
                ref var sunPosition = ref _positionPool.Get(sunEntity);
                ref var gravityPoint = ref _gravityPointPool.Get(sunEntity);
                ref var chargeRequest = ref _chargeContainerPool.Get(sunEntity).ChargeRequest;

                foreach (var entity in _entityFilter)
                {
                    ref var position = ref _positionPool.Get(entity);
                    ref var rotation = ref _rotationPool.Get(entity);
                    var chargeAmount = CalculateChargeCoefficient(position, rotation, sunPosition, gravityPoint) *
                                       chargeRequest.Value;

                    if (chargeAmount < 0.1f) continue;

                    if (_chargeRequestPool.Has(entity) == false)
                    {
                        _chargeRequestPool.Add(entity);
                    }

                    _chargeRequestPool.Get(entity).Value += chargeAmount;
                }
            }
        }

        private static float CalculateChargeCoefficient(in Position playerPosition, in Rotation playerRotation,
            in Position sunPosition, in GravityPoint gravityPoint)
        {
            var rotationCoefficient = Vector3.Dot(playerRotation.LookDir,
                sunPosition.Value - playerPosition.Value.normalized);
            if (rotationCoefficient > MIN_ROTATION_COEFICIENT)
            {
                return rotationCoefficient * WorldExtensions.CalculateDistanceCoefficient(playerPosition, sunPosition,
                    gravityPoint.InnerRadius, gravityPoint.OuterRadius);
            }

            return 0f;
        }

        public void Dispose()
        {
        }
    }
}