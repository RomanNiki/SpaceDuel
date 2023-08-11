using Core.EnergyLimits.Components;
using Core.Player.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.EnergyLimits.Systems
{
    public class DischargeSystem : ISystem
    {
        private Filter _filter;
        private Stash<Energy> _energyPool;
        private Stash<DischargeRequest> _dischargePool;
        private Stash<NoEnergyBlock> _noEnergyBlockPool;
        private Stash<EnergyChangedEvent> _energyChangedPool;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<Energy>().With<DischargeRequest>();
            _energyPool = World.GetStash<Energy>();
            _dischargePool = World.GetStash<DischargeRequest>();
            _noEnergyBlockPool = World.GetStash<NoEnergyBlock>();
            _energyChangedPool = World.GetStash<EnergyChangedEvent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                SpendEnergy(ref _energyPool.Get(entity), entity, _dischargePool.Get(entity).Value);
                _dischargePool.Remove(entity);
                _energyChangedPool.Add(entity);
            }
        }
        
        private void SpendEnergy(ref Energy energy, in Entity entity, in float energyLoss)
        {
            energy.Value = Mathf.Max(0.00f, energy.Value - energyLoss);
            if (energy.Value > 0f) return;
            if (_noEnergyBlockPool.Has(entity) == false)
            {
                _noEnergyBlockPool.Add(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}