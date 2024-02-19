using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Movement.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct ForceRequest : IComponent
    {
#if UNITY_WEBGL == false
        public EntityId EntityId;
#else
        public Entity Entity;
#endif
        public Vector2 Value;
    }
}