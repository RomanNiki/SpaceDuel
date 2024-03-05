using _Project.Develop.Runtime.Engine.UI.Statistics.Sliders;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.UI.Weapon
{
    public class WeaponsSliders : MonoBehaviour
    {
        [SerializeField] private BaseStatisticSlider _primarySlider;
        [SerializeField] private BaseStatisticSlider _secondarySlider;

        public void SetPrimarySliderData(float value, float maxValue)
        {
            _primarySlider.UpdateSliderData(value, maxValue, true);
        }

        public void SetSecondarySliderData(float value, float maxValue)
        {
            _secondarySlider.UpdateSliderData(value, maxValue, true);
        }
    }
}