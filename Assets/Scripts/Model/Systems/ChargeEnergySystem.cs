using Leopotam.Ecs;
using Model.Components.Extensions;
using Model.Components.Requests;
using Model.Components.Unit;

namespace Model.Systems
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
                energyComponent.ChargeEnergy(ref entity, dischargeRequest.Value);
            }
        }
    }
}