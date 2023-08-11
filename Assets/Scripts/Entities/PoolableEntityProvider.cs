using Zenject;

namespace Entities
{
    public class PoolableEntityProvider : EntityProvider, IPoolable<IMemoryPool>
    {
        private IMemoryPool _memoryPool;

        public void OnDespawned()
        {
            _memoryPool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _memoryPool = pool;
        }

        protected override void OnDispose()
        {
            PoolRecycle();
        }

        private void PoolRecycle()
        {
            _memoryPool.Despawn(this);
        }
    }
    
    public class PoolableEntityFactory : PlaceholderFactory<EntityProvider>
    {
    }
}