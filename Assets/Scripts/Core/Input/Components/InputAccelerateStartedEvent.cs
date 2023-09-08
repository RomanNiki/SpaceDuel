using System;
using Core.Characteristics.Enums;
using Scellecs.Morpeh;

namespace Core.Input.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct InputAccelerateStartedEvent : IComponent
    {
        public TeamEnum PlayerTeam;
    }
}