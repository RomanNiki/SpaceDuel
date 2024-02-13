using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Extensions;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.EntityPool;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class DischargeSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<Energy> _energyPool;
        private Stash<DischargeRequest> _dischargePool;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<DischargeRequest>().Build();
            _energyPool = World.GetStash<Energy>();
            _dischargePool = World.GetStash<DischargeRequest>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var dischargeRequestEntity in _filter)
            {
                ref var dischargeRequest = ref _dischargePool.Get(dischargeRequestEntity);
                ref var dischargeEntity = ref dischargeRequest.Entity;
                ref var energy = ref _energyPool.Get(dischargeEntity);
                ChangeEnergy(ref energy, dischargeRequest.Value);
                World.SendMessage(new EnergyChangedEvent { Entity = dischargeEntity });
            }
        }

        private static void ChangeEnergy(ref Energy energy, in float energyLoss) =>
            energy.Value = Mathf.Max(0.00f, energy.Value - energyLoss);
    }
}