using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class EnergyBlockSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<EnergyChangedEvent> _energyChangedEventPool;
        private Stash<NoEnergyBlock> _noEnergyBlockPool;
        private Stash<Energy> _energyPool;

        public override void OnAwake()
        {
            _filter = World.Filter.With<EnergyChangedEvent>().Build();
            _energyChangedEventPool = World.GetStash<EnergyChangedEvent>();
            _energyPool = World.GetStash<Energy>();
            _noEnergyBlockPool = World.GetStash<NoEnergyBlock>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var energyChangedEntity in _filter)
            {
                ref var energyChangedEvent = ref _energyChangedEventPool.Get(energyChangedEntity);
                ref var energyEntity = ref energyChangedEvent.Entity;
                
                if (energyEntity.IsNullOrDisposed())
                {
                    continue;
                }
                
                ref var energy = ref _energyPool.Get(energyEntity);
                var hasEnergyBlock = _noEnergyBlockPool.Has(energyEntity);
                
                switch (energy.Value)
                {
                    case > 0f when hasEnergyBlock:
                        _noEnergyBlockPool.Remove(energyEntity);
                        break;
                    case <= 0f when hasEnergyBlock == false:
                        _noEnergyBlockPool.Add(energyEntity);
                        break;
                }
            }
        }
    }
}