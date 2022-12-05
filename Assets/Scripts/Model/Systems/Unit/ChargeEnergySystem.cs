using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Components.Unit;

namespace Model.Systems.Unit
{
    public sealed class ChargeEnergySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Energy, ChargeRequest> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var energyComponent = ref _filter.Get1(i);
                ref var dischargeRequest = ref _filter.Get2(i);
                ref var entity = ref _filter.GetEntity(i);
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