using Model.Components.Extensions.Interfaces;
using Model.Systems.UI;
using UnityEngine;

namespace Views.Systems.Update
{
    public class
        UpdateBarViewSystem<TEvent, TValueComponent, TUIComponent> : UpdateUICharacteristicSystem<TEvent,
            TValueComponent, TUIComponent> where TUIComponent : struct, IBarContainer
        where TValueComponent : struct, ICharacteristic
        where TEvent : struct
    {
        protected override void UpdateUI(TValueComponent component, TUIComponent uiComponent)
        {
            var bar = uiComponent.Bar;
            bar.value = Mathf.Clamp(component.Current / component.Initial, bar.minValue, bar.maxValue);
        }
    }
}