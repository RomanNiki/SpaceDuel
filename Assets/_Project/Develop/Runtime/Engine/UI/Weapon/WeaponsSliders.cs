using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.UI.Weapon
{
    public class WeaponsSliders : MonoBehaviour
    {
        [SerializeField] private WeaponSlider _primarySlider;
        [SerializeField] private WeaponSlider _secondarySlider;

        public async UniTaskVoid SetPrimarySliderData(float value)
        {
            await _primarySlider.SetSliderValue(value);
        }

        public async UniTaskVoid SetSecondarySliderData(float value)
        {
            await _secondarySlider.SetSliderValue(value);
        }
    }
}