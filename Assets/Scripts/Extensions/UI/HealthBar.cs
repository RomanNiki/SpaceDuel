using Model.Extensions.Interfaces;
using UnityEngine.UI;

namespace Extensions.UI
{
    public struct HealthBar : IBarContainer
    {
        public Slider Bar { get; set; }
    }
}