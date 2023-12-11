﻿using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Weapon.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct WeaponType : IComponent
    {
        public WeaponEnum Value;
    }
}