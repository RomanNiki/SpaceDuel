using Core.Characteristics.EnergyLimits.Components;
using Core.Extensions;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class ChargeSystem : ISystem
    {
        private Filter _filter;
        private Stash<Energy> _energyPool;
        private Stash<ChargeRequest> _chargeRequestPool;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<ChargeRequest>().Build();
            _energyPool = World.GetStash<Energy>();
            _chargeRequestPool = World.GetStash<ChargeRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var requestEntity in _filter)
            {
                ref var chargeRequest = ref _chargeRequestPool.Get(requestEntity);
                ref var chargeRequestEntity = ref chargeRequest.Entity;
                ref var energy = ref _energyPool.Get(chargeRequestEntity);
                var chargeAmount = chargeRequest.Value;
                energy.Value = Mathf.Min(energy.MaxValue, energy.Value + chargeAmount);

                World.SendMessage(new EnergyChangedEvent { Entity = chargeRequestEntity });

                World.RemoveEntity(requestEntity);
            }
        }

        public void Dispose()
        {
        }
    }
}