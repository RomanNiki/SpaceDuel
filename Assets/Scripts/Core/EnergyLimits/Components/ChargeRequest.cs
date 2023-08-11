using System;
using Scellecs.Morpeh;

namespace Core.EnergyLimits.Components
{
    [Serializable]
    public struct ChargeRequest : IComponent
    {
        public float Value;
    }
}