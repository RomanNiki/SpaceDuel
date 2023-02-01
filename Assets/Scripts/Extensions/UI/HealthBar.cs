using System;
using UnityEngine.UI;

namespace Extensions.UI
{
    [Serializable]
    public struct HealthBar : IBarContainer
    {
        public Slider Bar { get; set; }
    }
}