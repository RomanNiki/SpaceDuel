using Model.Extensions.Interfaces;
using UnityEngine.UI;

namespace Extensions.UI
{
    public struct EnergyBar : IBarContainer
    {
        public Slider Bar { get; set; }
    }
}