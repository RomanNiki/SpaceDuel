﻿using Core.Common.Enums;

namespace Core.Weapon.Components
{
    using System;
    using Scellecs.Morpeh;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ShootObjectType : IComponent
    {
        public ObjectId ObjectId;
    }
}