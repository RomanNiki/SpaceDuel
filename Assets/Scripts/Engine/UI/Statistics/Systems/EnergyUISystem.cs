using Core.Characteristics.EnergyLimits.Components;
using Core.Weapon.Components;
using Engine.UI.Statistics.Services;
using Engine.Views.Components;

namespace Engine.UI.Statistics.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class EnergyUISystem : ISystem
    {
        private Filter _energyChangedFilter;
        private Filter _uiFilter;
        private Stash<EnergyChangedEvent> _energyChangedStash;
        private Stash<Owner> _ownerStash;
        private Stash<Energy> _energyStash;
        private Stash<UnityComponent<EnergySlider>> _sliderStash;
        public World World { get; set; }

        public void OnAwake()
        {
            _energyChangedStash = World.GetStash<EnergyChangedEvent>();
            _ownerStash = World.GetStash<Owner>();
            _energyStash = World.GetStash<Energy>();
            _sliderStash = World.GetStash<UnityComponent<EnergySlider>>();
            _energyChangedFilter = World.Filter.With<EnergyChangedEvent>().Build();
            _uiFilter = World.Filter.With<UnityComponent<EnergySlider>>().With<Owner>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var eventEntity in _energyChangedFilter)
            {
                ref var owner = ref _energyChangedStash.Get(eventEntity).Entity;
                foreach (var uiEntity in _uiFilter)
                {
                    var uiOwnerEntity = _ownerStash.Get(uiEntity).Entity;
                    if (owner.Equals(uiOwnerEntity))
                    {
                        ref var energy = ref _energyStash.Get(uiOwnerEntity);
                        _sliderStash.Get(uiEntity).Value.UpdateSliderData(energy.Value, energy.MaxValue);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}