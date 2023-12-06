using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Extensions;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class ChargeSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<Energy> _energyPool;
        private Stash<ChargeRequest> _chargeRequestPool;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<ChargeRequest>().Build();
            _energyPool = World.GetStash<Energy>();
            _chargeRequestPool = World.GetStash<ChargeRequest>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var requestEntity in _filter)
            {
                ref var chargeRequest = ref _chargeRequestPool.Get(requestEntity);
                ref var chargeRequestEntity = ref chargeRequest.Entity;
                if (chargeRequestEntity.IsNullOrDisposed())
                {
                    continue;
                }
                ref var energy = ref _energyPool.Get(chargeRequestEntity);
                var chargeAmount = chargeRequest.Value * deltaTime;
                var targetEnergy =  Mathf.Min(energy.MaxValue, energy.Value + chargeAmount);

                energy.Value = targetEnergy;

                World.SendMessage(new EnergyChangedEvent { Entity = chargeRequestEntity });
            }
        }
    }
}