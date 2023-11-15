using _Project.Develop.Modules.Pooling.Core.Pool;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.View
{
    [RequireComponent(typeof(EntityProvider))]
    public class PoolMonoProvider : EntityProviderBase, IPoolItem
    {
        private EntityProvider _entityProvider;
        private IPool _memoryPool;

        private void Awake()
        {
            _entityProvider = GetComponent<EntityProvider>();
        }

        public void Dispose()
        {
            _entityProvider.Dispose();
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

        public override void Resolve(World world, Entity entity)
        {
            _entityProvider.Resolve(world, entity);
        }

        private void PoolRecycle()
        {
            _memoryPool?.Despawn(this);
        }
    }
}