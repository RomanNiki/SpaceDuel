﻿using System;
using Core.Characteristics.EnergyLimits.Components;
using Engine.Converters.Base;

namespace Engine.Converters.EnergyLimits
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public class EnergyConverter : Converter<Energy>
    {
    }
}