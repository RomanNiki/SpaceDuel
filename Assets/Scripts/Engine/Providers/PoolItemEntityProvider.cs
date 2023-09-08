using Modules.Pooling.Pool;

namespace Engine.Providers
{
    public class PoolItemEntityProvider : EntityProvider, IPoolItem
    {
        private IPool _memoryPool;

        public void OnDespawned()
        {
            _memoryPool = null;
        }

        public void OnSpawned(IPool pool)
        {
            _memoryPool = pool;
        }

        protected override void OnDispose()
        {
            PoolRecycle();
        }

        private void PoolRecycle()
        {
            _memoryPool?.Despawn(this);
        }
    }
}