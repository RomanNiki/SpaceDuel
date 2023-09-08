using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Movement.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct Rotation : IComponent
    {
        public float Value;
        public Vector3 LookDir => Quaternion.Euler(0f, 0f, Value) * Vector3.up;
    }
}