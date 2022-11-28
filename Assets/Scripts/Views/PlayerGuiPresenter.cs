using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class PlayerGuiPresenter : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _energySlider;

        [SerializeField] private Vector3 _offSet;
        

        private void OnEnergyChanged(float value)
        {
            _energySlider.value = Mathf.Clamp(value, _energySlider.minValue, _energySlider.maxValue);
        }

        private void OnHealthChanged(float value)
        {
            _healthSlider.value = Mathf.Clamp(value, _energySlider.minValue, _energySlider.maxValue);
        }
    }
}