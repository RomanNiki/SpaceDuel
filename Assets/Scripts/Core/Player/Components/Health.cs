using Core.Extensions;
using Scellecs.Morpeh;

namespace Core.Player.Components
{
    public struct Health : IStat, IComponent
    {
        public float MaxValue { get; set; }
        public float Value { get; set; }

        public Health(float maxValue)
        {
            MaxValue = maxValue;
            Value = MaxValue;
        }
    }
}