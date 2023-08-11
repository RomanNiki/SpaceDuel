using Core.Extensions;
using Scellecs.Morpeh;

namespace Core.Player.Components
{
    public struct Energy : IStat, IComponent
    {
        public Energy(float maxValue)
        {
            MaxValue = maxValue;
            Value = maxValue;
        }

        public float MaxValue { get; set; }
        public float Value { get; set; }
    }
}