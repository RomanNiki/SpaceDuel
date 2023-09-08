using System;
using Scellecs.Morpeh;

namespace Core.Extensions.Factories
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct EntityFactoryRef<T> : IComponent
        where T : IEntityFactory
    {
        public EntityFactoryRef(T factory)
        {
            Factory = factory;
        }

        public T Factory;
    }
}