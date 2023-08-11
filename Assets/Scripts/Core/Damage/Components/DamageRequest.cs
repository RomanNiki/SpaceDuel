using System;
using Scellecs.Morpeh;

namespace Core.Damage.Components
{
    [Serializable]
    public struct DamageRequest : IComponent
    {
        public float Value;
    }
}