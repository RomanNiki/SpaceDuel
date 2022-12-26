using Leopotam.Ecs;
using Model.Components.Extensions.Interfaces;

namespace Model.Systems.UI
{
    public abstract class UpdateUICharacteristicSystem<TEvent, TValueComponent, TUIComponent> : IEcsRunSystem
        where TEvent : struct
        where TValueComponent : struct
        where TUIComponent : struct
    {
        protected readonly EcsFilter<TValueComponent, TUIComponent, TEvent> ComponentUpdateFilter = null;

        public void Run()
        {
            foreach (var i in ComponentUpdateFilter)
            {
                ref var component = ref ComponentUpdateFilter.Get1(i);
                ref var bar = ref ComponentUpdateFilter.Get2(i);
                UpdateUI(component, bar);
            }
        }

        protected abstract void UpdateUI(TValueComponent component, TUIComponent uiComponent);
    }
}