using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Common;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Movement.Components.Gravity;
using _Project.Develop.Runtime.Core.Timers.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;
using WorldExtensions = _Project.Develop.Runtime.Core.Extensions.WorldExtensions;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class SunChargeSystem : UpdateSystem
    {

        private Filter _entityFilter;
        private Filter _sunFilter;
        private Stash<Rotation> _rotationPool;
        private Stash<Position> _positionPool;
        private Stash<ChargeContainer> _chargeContainerPool;
        private Stash<GravityPoint> _gravityPointPool;
        
        public override void OnAwake()
        {
            _entityFilter = World.Filter.With<Energy>().With<Position>().With<Rotation>().Without<SunDischargeTag>()
                .Without<Timer<InvisibleTimer>>().Without<NonСhargeableTag>().Build();
            _sunFilter = World.Filter.With<GravityPoint>().With<Position>().With<ChargeContainer>().Build();
            _positionPool = World.GetStash<Position>();
            _rotationPool = World.GetStash<Rotation>();
            _chargeContainerPool = World.GetStash<ChargeContainer>();
            _gravityPointPool = World.GetStash<GravityPoint>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var sunEntity in _sunFilter)
            {
                ref var sunPosition = ref _positionPool.Get(sunEntity);
                ref var gravityPoint = ref _gravityPointPool.Get(sunEntity);
                ref var chargeSpeed = ref _chargeContainerPool.Get(sunEntity).Value;

                foreach (var entity in _entityFilter)
                {
                    ref var position = ref _positionPool.Get(entity);
                    ref var rotation = ref _rotationPool.Get(entity);
                    var chargeAmount = CalculateChargeCoefficient(position, rotation, sunPosition, gravityPoint) *
                                       chargeSpeed * deltaTime;
                    if (chargeAmount < GameConfig.MinChargeAmount) continue;

                    World.SendMessage(new ChargeRequest { Entity = entity, Value = chargeAmount });
                }
            }
        }

        private static float CalculateChargeCoefficient(in Position playerPosition, in Rotation playerRotation,
            in Position sunPosition, in GravityPoint gravityPoint)
        {
            var rotationCoefficient = Vector3.Dot(playerRotation.LookDir,
                sunPosition.Value - playerPosition.Value.normalized);
            if (rotationCoefficient > GameConfig.MinRotationCoefficient)
            {
                return rotationCoefficient * WorldExtensions.CalculateDistanceCoefficient(playerPosition, sunPosition,
                    gravityPoint.InnerRadius, gravityPoint.OuterRadius);
            }

            return 0f;
        }
    }
}