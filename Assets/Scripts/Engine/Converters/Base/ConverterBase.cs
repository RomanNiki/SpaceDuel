using System;
using Scellecs.Morpeh;

namespace Engine.Converters.Base
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public abstract class ConverterBase : IConverter
    {
        public abstract void Resolve(World world, Entity entity);
    }
}