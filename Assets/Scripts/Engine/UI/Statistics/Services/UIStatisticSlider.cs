using UnityEngine;
using UnityEngine.UI;

namespace Engine.UI.Statistics.Services
{
    public class UIStatisticSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void UpdateSliderData(float value, float maxValue)
        {
            if (maxValue != 0)
            {
                _slider.value = value / maxValue;
            }
        }
    }
}