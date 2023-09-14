﻿using System;
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
    public struct ForceRequest : IComponent
    {
        public EntityId EntityId;
        public Vector2 Value;
    }
}