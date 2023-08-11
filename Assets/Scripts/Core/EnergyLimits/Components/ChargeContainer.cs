using System;
using Scellecs.Morpeh;

namespace Core.EnergyLimits.Components
{
    [Serializable]
    public struct ChargeContainer : IComponent
    {
        public ChargeRequest ChargeRequest;
    }
}