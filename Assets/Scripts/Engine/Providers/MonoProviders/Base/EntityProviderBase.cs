using Scellecs.Morpeh;

namespace Engine.Providers.MonoProviders.Base
{
    public abstract class EntityProviderBase : MonoProviderBase
    {
        public abstract override void Resolve(World world, Entity entity);
    }
}