using Leopotam.Ecs;
using Model.Unit.EnergySystems.Components;
using Model.Unit.EnergySystems.Components.Events;
using Model.Unit.EnergySystems.Components.Requests;
using UnityEngine;

namespace Model.Unit.EnergySystems
{
    public sealed class DischargeEnergySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Energy, DischargeRequest> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var energyComponent = ref _filter.Get1(i);
                ref var dischargeRequest = ref _filter.Get2(i);
                ref var entity = ref _filter.GetEntity(i);
                SpendEnergy(ref energyComponent, ref entity, dischargeRequest.Value);
                entity.Get<EnergyChangedEvent>();
            }
        }

        private static void SpendEnergy(ref Energy energy, ref EcsEntity entity, in float energyLoss)
        {
            energy.Current = Mathf.Max(0.0f, energy.Current - energyLoss);
            if (energy.Current <= 0f)
            {
                entity.Get<NoEnergyBlock>();
            }
        }
    }
}