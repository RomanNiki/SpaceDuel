using Leopotam.Ecs;

namespace Model.Systems.UI
{
    public abstract class UpdateUICharacteristicSystem<TEvent, TValueComponent, TUIComponent> : IEcsRunSystem
        where TEvent : struct
        where TValueComponent : struct
        where TUIComponent : struct
    {
        protected readonly EcsFilter<TValueComponent, TUIComponent, TEvent> _componentUpdateFilter = null;

        public void Run()
        {
            foreach (var i in _componentUpdateFilter)
            {
                ref var component = ref _componentUpdateFilter.Get1(i);
                ref var bar = ref _componentUpdateFilter.Get2(i);
                UpdateUI(component, bar);
            }
        }

        protected abstract void UpdateUI(TValueComponent component, TUIComponent uiComponent);
    }
}