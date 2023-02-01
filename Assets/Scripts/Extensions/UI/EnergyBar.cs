using System;
using UnityEngine.UI;

namespace Extensions.UI
{
    [Serializable]
    public struct EnergyBar : IBarContainer
    {
        public Slider Bar { get; set; }
    }
}