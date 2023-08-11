using Scellecs.Morpeh;

namespace Core.Extensions.Factories
{
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