using Model.Components.Events;
using Model.Components.Extensions.UI;
using Model.Components.Unit;
using Model.Systems.UI;
using UnityEngine;

namespace Views.Systems.Update
{
    public class UpdateEnergyBarViewSystem : UpdateUICharacteristicSystem<EnergyChangedEvent, Energy, CharacteristicBars>
    {
        protected override void UpdateUI(Energy component, CharacteristicBars uiComponent)
        {
            var bar = uiComponent.EnergyBar;
            bar.value = Mathf.Clamp(component.Current % bar.maxValue + 1, bar.minValue, bar.maxValue);
        }
    }
}