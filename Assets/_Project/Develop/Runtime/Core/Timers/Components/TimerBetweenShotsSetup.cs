using System;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Timers.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct TimerBetweenShotsSetup : IComponent
    {
        public float TimeSec;
    }
}