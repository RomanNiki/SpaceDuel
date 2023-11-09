using Core.Characteristics.Damage.Components;
using Core.Weapon.Components;
using Engine.UI.Statistics.Services;
using Engine.Views.Components;
using Scellecs.Morpeh;

namespace Engine.UI.Statistics.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class HealthUISystem : ISystem
    {
        public World World { get; set; }
        private Filter _healthChangedFilter;
        private Filter _uiFilter;
        private Stash<HealthChangedEvent> _healthChangedStash;
        private Stash<Owner> _ownerStash;
        private Stash<Health> _healthStash;
        private Stash<UnityComponent<HealthSlider>> _sliderStash;

        public void OnAwake()
        {
            _healthChangedStash = World.GetStash<HealthChangedEvent>();
            _ownerStash = World.GetStash<Owner>();
            _healthStash = World.GetStash<Health>();
            _sliderStash = World.GetStash<UnityComponent<HealthSlider>>();
            _healthChangedFilter = World.Filter.With<HealthChangedEvent>().Build();
            _uiFilter = World.Filter.With<UnityComponent<HealthSlider>>().With<Owner>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var eventEntity in _healthChangedFilter)
            {
                ref var owner = ref _healthChangedStash.Get(eventEntity).Entity;
                foreach (var uiEntity in _uiFilter)
                {
                    var uiOwnerEntity = _ownerStash.Get(uiEntity).Entity;
                    if (owner.Equals(uiOwnerEntity))
                    {
                        ref var health = ref _healthStash.Get(uiOwnerEntity);
                        _sliderStash.Get(uiEntity).Value.UpdateSliderData(health.Value, health.MaxValue);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}