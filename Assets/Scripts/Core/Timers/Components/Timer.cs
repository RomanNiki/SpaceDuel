using System;
using Scellecs.Morpeh;

namespace Core.Timers.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct Timer<TTimerFlag> : IComponent
    where TTimerFlag : struct, IComponent
    {
        public float TimeLeftSec;
    }
}