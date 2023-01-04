using Model.Components.Extensions.Interfaces;
using UnityEngine.UI;

namespace Model.Components.Extensions.UI
{
    public struct EnergyBar : IBarContainer
    {
        public Slider Bar { get; set; }
    }
}