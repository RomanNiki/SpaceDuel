using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base
{
    public interface IProvider
    {
        void Resolve(World world, Entity entity);
    }
}