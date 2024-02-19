using System;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Timers.Components
{
    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct InvisibleTimerSetup : IComponent
    {
        public float TimeSec;
    }
}