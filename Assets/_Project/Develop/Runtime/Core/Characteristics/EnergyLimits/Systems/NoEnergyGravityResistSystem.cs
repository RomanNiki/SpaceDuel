﻿using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Movement.Components.Gravity;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class NoEnergyGravityResistSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<GravityResistTag> _gravityResistPool;
        private Stash<Energy> _energyPool;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GravityResistTag>().With<Energy>().Build();
            _gravityResistPool = World.GetStash<GravityResistTag>();
            _energyPool = World.GetStash<Energy>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                if (_energyPool.Get(entity).HasEnergy)
                    continue;
                _gravityResistPool.Remove(entity);
            }
        }
    }
}