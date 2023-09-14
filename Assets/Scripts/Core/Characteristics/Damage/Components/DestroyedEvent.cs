using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Characteristics.Damage.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    public struct DestroyedEvent<TTag> : IComponent
        where TTag : struct, IComponent

    {
        public Vector2 Position;
    }
}