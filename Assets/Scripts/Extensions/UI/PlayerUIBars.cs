using UnityEngine;
using UnityEngine.UI;

namespace Extensions.UI
{
    public class PlayerUIBars : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Slider _energyBar;
        [SerializeField] private Vector2 _offSet;
        
        public Slider HealthBar => _healthBar;
        public Slider EnergyBar => _energyBar;
        public Vector2 OffSet => _offSet;
    }
}