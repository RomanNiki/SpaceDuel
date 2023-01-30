using Leopotam.Ecs;
using Model.Unit.EnergySystems.Components;
using Model.Unit.EnergySystems.Components.Events;
using Model.Unit.EnergySystems.Components.Requests;

namespace Model.Unit.EnergySystems
{
    public sealed class ChargeEnergySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Energy, ChargeRequest> _chargeFilter;
        
        public void Run()
        {
            foreach (var i in _chargeFilter)
            {
                ref var energyComponent = ref _chargeFilter.Get1(i);
                ref var dischargeRequest = ref _chargeFilter.Get2(i);
                ref var entity = ref _chargeFilter.GetEntity(i);
                ChargeEnergy(ref energyComponent, ref entity, dischargeRequest.Value);
                entity.Get<EnergyChangedEvent>();
            }
        }
        
        private static void ChargeEnergy(ref Energy energy, ref EcsEntity entity, float amount)
        {
            if (energy.Initial > energy.Current)
            {
                energy.Current += amount;
            }

            if (energy.Current > 0f && entity.Has<NoEnergyBlock>())
            {
                entity.Del<NoEnergyBlock>();
            }
        }
    }
}