using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Weapon.Components;
using _Project.Develop.Runtime.Engine.Common.Components;
using _Project.Develop.Runtime.Engine.UI.Statistics.Sliders;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.ECS.UI.PlayerUI.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public abstract class UISliderUpdateSystem<TEvent, TStat, TSlider> : ISystem
    where TEvent : struct, IComponent
    where TStat : struct, IComponent, IStat
    where TSlider : BaseStatisticSlider
    {
        private Stash<TEvent> _eventStash;
        private Filter _healthChangedFilter;
        private Stash<TStat> _healthStash;
        private Stash<Owner> _ownerStash;
        private Stash<UnityComponent<TSlider>> _sliderStash;
        private Filter _uiFilter;
        public World World { get; set; }

        public void OnAwake()
        {
            _eventStash = World.GetStash<TEvent>();
            _ownerStash = World.GetStash<Owner>();
            _healthStash = World.GetStash<TStat>();
            _sliderStash = World.GetStash<UnityComponent<TSlider>>();
            _healthChangedFilter = World.Filter.With<TEvent>().Build();
            _uiFilter = World.Filter.With<UnityComponent<TSlider>>().With<Owner>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var eventEntity in _healthChangedFilter)
            {
                ref var owner = ref GetEventOwner(_eventStash, eventEntity);
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

        protected abstract ref Entity GetEventOwner(Stash<TEvent> eventStash, Entity eventEntity);
    }
}