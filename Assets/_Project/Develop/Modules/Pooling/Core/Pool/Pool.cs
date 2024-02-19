using _Project.Develop.Modules.Pooling.Core.Factory;

namespace _Project.Develop.Modules.Pooling.Core.Pool
{
    public class Pool<TComponent> : PoolBase<TComponent>, IPool<TComponent>, IFactory<TComponent>
        where TComponent : IPoolItem
    {
        public Pool(Settings settings, IFactory<TComponent> factory) : base(settings, factory)
        {
        }

        public Pool(IFactory<TComponent> factory) : base(Settings.Default, factory)
        {
        }

        public TComponent Create()
        {
            return Spawn();
        }

        public TComponent Spawn()
        {
            var item = GetInternal();
            ReInitialize(item);
            return item;
        }

        protected virtual void ReInitialize(TComponent item)
        {
            item.OnSpawned(this);
        }
    }
}