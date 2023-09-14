using Scellecs.Morpeh;

namespace Engine.Providers.MonoProviders.Base
{
    public interface IProvider
    {
        void Resolve(World world, Entity entity);
    }
}