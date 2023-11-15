using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Common
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct Team : IComponent
    {
        public TeamEnum Value;
    }
}