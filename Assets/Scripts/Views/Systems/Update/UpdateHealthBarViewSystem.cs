using Model.Components.Events;
using Model.Components.Extensions.UI;
using Model.Components.Unit;
using Model.Systems.UI;
using UnityEngine;

namespace Views.Systems.Update
{
    public class UpdateHealthBarViewSystem : UpdateUICharacteristicSystem<HealthChangeEvent, Health, CharacteristicBars>
    {
        protected override void UpdateUI(Health component, CharacteristicBars uiComponent)
        { 
            var bar = uiComponent.HealthBar;
            bar.value = Mathf.Clamp(  component.Current % bar.maxValue + 1, bar.minValue, bar.maxValue);
        }
    }
}