using Cysharp.Threading.Tasks;
using Modules.Pooling.Core.Factory;

namespace Modules.Pooling.Core.Pool
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

        public async UniTask<TComponent> Spawn()
        {
            var item = await GetInternal();
            ReInitialize(item);
            return item;
        }

        protected virtual void ReInitialize(TComponent item)
        {
            item.OnSpawned(this);
        }

        public async UniTask<TComponent> Create() => await Spawn();
    }
}