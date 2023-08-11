using Core.EnergyLimits.Components;
using Core.Player.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.EnergyLimits.Systems
{
    public sealed class ChargeSystem : ISystem
    {
        private Filter _filter;
        private Stash<Energy> _energyPool;
        private Stash<ChargeRequest> _chargeRequestPool;
        private Stash<EnergyChangedEvent> _energyChangedPool;
        private Stash<NoEnergyBlock> _noEnergyBlockPool;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Energy>().With<ChargeRequest>();
            _energyPool = World.GetStash<Energy>();
            _chargeRequestPool = World.GetStash<ChargeRequest>();
            _energyChangedPool = World.GetStash<EnergyChangedEvent>();
            _noEnergyBlockPool = World.GetStash<NoEnergyBlock>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var energy = ref _energyPool.Get(entity);
                ref var chargeAmount = ref _chargeRequestPool.Get(entity).Value;
                energy.Value = Mathf.Min(energy.MaxValue, energy.Value + chargeAmount);

                if (_energyChangedPool.Has(entity) == false)
                {
                    _energyChangedPool.Add(entity);
                }

                _chargeRequestPool.Remove(entity);

                if (_noEnergyBlockPool.Has(entity))
                {
                    _noEnergyBlockPool.Remove(entity);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}