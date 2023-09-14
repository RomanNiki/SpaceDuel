using Cysharp.Threading.Tasks;
using Modules.Pooling.Core.Factory;
using UnityEngine;

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

        public TComponent Spawn(Vector3 position = new(), float rotation = 0)
        {
            var item = GetInternal();
            ReInitialize(item);
            OnSpawn(item, position, rotation);
            return item;
        }

        protected virtual void ReInitialize(TComponent item)
        {
            item.OnSpawned(this);
        }

        public TComponent Create(Vector3 position = new(), float rotation = 0) => Spawn(position, rotation);

        protected virtual void OnSpawn(TComponent item, Vector3 position, float rotation)
        {
        }

        public async UniTask Load()
        {
            await LoadInternal();
        }
    }
}