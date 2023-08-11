using System;
using Core.EnergyLimits.Components;
using Scellecs.Morpeh;

namespace Core.Weapon.Components
{
    [Serializable]
    public struct DischargeContainer : IComponent
    {
        public DischargeRequest DischargeRequest;
    }
}