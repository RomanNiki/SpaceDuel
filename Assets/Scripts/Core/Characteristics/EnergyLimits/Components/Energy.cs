using System;
using Core.Extensions;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Characteristics.EnergyLimits.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct Energy : IStat, IComponent
    {
        public Energy(float maxCurrentValue, float minValue = 0)
        {
            MaxValue = maxCurrentValue;
            Value = maxCurrentValue;
            BaseValue = maxCurrentValue;
            MinValue = minValue;
        }

        [field: SerializeField] public float MaxValue { get; set; }
        [field: SerializeField] public float Value { get; set; }
        [field: SerializeField] public float BaseValue { get; set; }
        [field: SerializeField] public float MinValue { get; set; }
    }
}