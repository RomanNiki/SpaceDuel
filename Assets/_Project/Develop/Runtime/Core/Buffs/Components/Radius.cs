using System;
using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
namespace _Project.Develop.Runtime.Core.Buffs.Components
{
    [Serializable]
    public struct Radius : IComponent
    {
        public float Value;
    }
}