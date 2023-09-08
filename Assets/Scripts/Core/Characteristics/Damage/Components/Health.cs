using System;
using Core.Extensions;
using Scellecs.Morpeh;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Core.Characteristics.Damage.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct Health : IStat, IComponent
    {
        public Health(float maxValue, float minValue = 0)
        {
            MaxValue = maxValue;
            Value = maxValue;
            BaseValue = maxValue;
            MinValue = minValue;
        }

        [field: SerializeField] public float MaxValue { get; set; }
        [field: SerializeField] public float Value { get; set; }
        [field: SerializeField] public float BaseValue { get; set; }
        [field: SerializeField] public float MinValue { get; set; }
    }
}