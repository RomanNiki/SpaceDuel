using Engine.Providers.MonoProviders.Base;
using Modules.Pooling.Core.Pool;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.View
{
    public class PoolMonoProvider : EntityProviderBase, IPoolItem
    {
        [SerializeField] private EntityProvider _entityProvider;
        private IPool _memoryPool;

        public override void Resolve(World world, Entity entity)
        {
            _entityProvider.Resolve(world, entity);
        }

        public void Dispose()
        {
            _entityProvider.Dispose();
        }

        private void PoolRecycle()
        {
            _memoryPool?.Despawn(this);
        }

        public void OnSpawned(IPool pool)
        {
            _memoryPool = pool;
            _entityProvider.EntityDispose += PoolRecycle;
        }

        public void OnDespawned()
        {
            _memoryPool = null;
            _entityProvider.EntityDispose -= PoolRecycle;
        }
    }
}