using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Extensions;
using Model.Components.Requests;
using Model.Components.Unit;

namespace Model.Systems
{
    public sealed class DischargeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Energy, DischargeRequest> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var energyComponent = ref _filter.Get1(i);
                ref var dischargeRequest = ref _filter.Get2(i);
                ref var entity = ref _filter.GetEntity(i);
                energyComponent.SpendEnergy(ref entity, dischargeRequest.Value);
                entity.Get<EnergyEndedEvent>();
            }
        }
    }
}