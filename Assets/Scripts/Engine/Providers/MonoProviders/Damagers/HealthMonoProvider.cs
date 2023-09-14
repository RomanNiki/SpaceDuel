﻿using System;
using Core.Characteristics.Damage.Components;
using Engine.Providers.MonoProviders.Base;

namespace Engine.Providers.MonoProviders.Damagers
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public class HealthMonoProvider : MonoProvider<Health>
    {
    }
}