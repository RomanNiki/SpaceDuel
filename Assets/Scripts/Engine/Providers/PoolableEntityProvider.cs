using Modules.Pooling.Core.Pool;

namespace Engine.Providers
{
    public class PoolableEntityProvider : EntityProvider, IPoolItem
    {
        private IPool _memoryPool;

        public void OnDespawned() => _memoryPool = null;

        public void OnSpawned(IPool pool) => _memoryPool = pool;
        
        protected override void OnDispose() => PoolRecycle();
        
        private void PoolRecycle() => _memoryPool?.Despawn(this);
    }
}