using System;
using Core.Characteristics.EnergyLimits.Components;
using Scellecs.Morpeh;

namespace Core.Weapon.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct DischargeContainer : IComponent
    {
        public float Value;
    }
}