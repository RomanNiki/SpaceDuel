﻿using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.ECS.Effects.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct MaterialIndicator : IComponent
    {
        public Material Material;
        public Vector4 StartColor;
        public Vector4 EndColor;
        public float Intencity;
    }
}