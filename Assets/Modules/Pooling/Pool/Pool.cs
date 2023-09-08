using Cysharp.Threading.Tasks;
using Modules.Pooling.Factory;

namespace Modules.Pooling.Pool
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

        TComponent IFactory<TComponent>.Create()
        {
            return Spawn();
        }

        public async UniTask Load()
        {
            await LoadInternal();
        }
    }
}