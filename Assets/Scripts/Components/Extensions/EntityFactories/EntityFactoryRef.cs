using Presenters;

namespace Components.Extensions.EntityFactories
{
    public struct EntityFactoryRef<T>
        where T : BulletPresenter.Factory
    {
        public T Value;
    }
}