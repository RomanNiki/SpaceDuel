using System;
using _Project.Develop.Runtime.Engine.Services;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.ECS.Effects.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    [Serializable]
    public struct AcceleratePlayerEffect : IComponent
    {
        public AccelerateEffectController EffectController;
    }
}