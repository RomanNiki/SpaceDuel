﻿using System;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.Serialization;

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
#if MORPEH_BURST == false
        public Entity Entity;
#else
        public EntityId EntityId;
#endif
        public Vector2 Value;
    }
}