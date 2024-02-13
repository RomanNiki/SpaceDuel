using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Statistics.Services
{
    public class BaseStatisticSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void UpdateSliderData(float value, float maxValue)
        {
            if (maxValue != 0)
            {
                _slider.SetValueWithoutNotify(value / maxValue);
            }
        }
    }
}