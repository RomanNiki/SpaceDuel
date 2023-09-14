using System;
using Scellecs.Morpeh;
using Object = UnityEngine.Object;

namespace Engine.Views.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct UnityComponent<T> : IComponent
        where T : Object
    {
        public UnityComponent(T component)
        {
            Value = component;
        }

        public T Value;
    }
}