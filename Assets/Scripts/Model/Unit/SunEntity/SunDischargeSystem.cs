using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.EnergySystems.Components;
using Model.Unit.EnergySystems.Components.Requests;
using Model.Unit.Movement.Components;
using Model.Unit.SunEntity.Components;

namespace Model.Unit.SunEntity
{
    public class SunDischargeSystem : IEcsRunSystem
    {
        private readonly SunChargeSystem.Settings _settings;
        private readonly EcsFilter<SunDischargeTag, Position, Energy> _dischargeFilter = null;
        private readonly EcsFilter<Sun, Position, ChargeContainer> _sunFilter = null;

        public void Run()
        {
            foreach (var j in _sunFilter)
            {
                ref var sunPosition = ref _sunFilter.Get2(j);
                ref var chargeAmount = ref _sunFilter.Get3(j).ChargeRequest.Value;
                foreach (var i in _dischargeFilter)
                {
                    ref var dischargePosition = ref _dischargeFilter.Get2(i);
                    ref var entity = ref _dischargeFilter.GetEntity(i);

                    entity.Get<DischargeRequest>().Value +=
                        WorldExtensions.CalculateDistanceCoefficient(dischargePosition, sunPosition,
                            _settings.MinChargeDistance, _settings.MaxChargeDistance) * chargeAmount;
                }
            }
        }
    }
}