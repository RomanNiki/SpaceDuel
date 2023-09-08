﻿namespace Core.Characteristics.EnergyLimits.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class LifeCycleDischargeSystem : ISystem
    {
        public World World { get; set; }

        public void OnAwake()
        {
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
        }
    }
}