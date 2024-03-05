using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Statistics.Sliders
{
    public class BaseStatisticSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public event Action<float> SliderValueChanged;

        private void OnEnable()
        {
            SliderValueChanged?.Invoke(_slider.value);
        }

        public void UpdateSliderData(float value, float maxValue, bool invert = false)
        {
            if (maxValue <= 0f) return;
            
            var sliderValue = value / maxValue;
            if (invert) sliderValue = 1f - sliderValue;
            
            _slider.SetValueWithoutNotify(sliderValue);
            SliderValueChanged?.Invoke(sliderValue);
        }
    }
}