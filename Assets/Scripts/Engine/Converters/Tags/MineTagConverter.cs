﻿using System;
using Core.Weapon.Components;
using Engine.Converters.Base;

namespace Engine.Converters.Tags
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public class MineTagConverter : TagConverter<MineTag>
    {
    }
}