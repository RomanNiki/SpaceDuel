﻿using System;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Movement.Components.Events
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct StopAccelerationEvent : IComponent
    {
        public Entity Entity;
    }
}