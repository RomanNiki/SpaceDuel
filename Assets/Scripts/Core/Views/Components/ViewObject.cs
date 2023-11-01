using System;
using Core.Extensions.Views;
using Scellecs.Morpeh;

namespace Core.Views.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct ViewObject : IComponent
    {
        public IViewObject Value;
    }
}