using System.Text;
using Core.Characteristics.EnergyLimits.Components;
using Core.Extensions;
using Core.Movement.Components;
using Core.Movement.Gravity.Components;
using Core.Timers.Components;

namespace Core.Characteristics.EnergyLimits.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class SunDischargeSystem : ISystem
    {
        private const float MIN_DISCHARGE_AMOUNT = 0.01f;
        private Filter _entityFilter;
        private Filter _sunFilter;
        private Stash<Position> _positionPool;
        private Stash<ChargeContainer> _chargeContainerPool;
        private Stash<GravityPoint> _gravityPointPool;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _entityFilter = World.Filter.With<Energy>().With<Position>().With<SunDischargeTag>().Without<Timer<InvisibleTimer>>().Build();
            _sunFilter = World.Filter.With<GravityPoint>().With<Position>().With<ChargeContainer>().Build();
            _positionPool = World.GetStash<Position>();
            _chargeContainerPool = World.GetStash<ChargeContainer>();
            _gravityPointPool = World.GetStash<GravityPoint>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var sunEntity in _sunFilter)
            {
                ref var sunPosition = ref _positionPool.Get(sunEntity);
                ref var gravityPoint = ref _gravityPointPool.Get(sunEntity);
                ref var chargeRequest = ref _chargeContainerPool.Get(sunEntity).Value;

                foreach (var entity in _entityFilter)
                {
                    ref var position = ref _positionPool.Get(entity);
                    var dischargeAmount = Extensions.WorldExtensions.CalculateDistanceCoefficient(position, sunPosition,
                        gravityPoint.InnerRadius, gravityPoint.OuterRadius) * chargeRequest;

                    if (dischargeAmount < MIN_DISCHARGE_AMOUNT) continue;

                    World.SendMessage(new DischargeRequest { Entity = entity, Value = dischargeAmount });
                }
            }
        }

        public void Dispose()
        {
        }
    }
}