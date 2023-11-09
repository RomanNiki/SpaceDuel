using System;
using Engine.Providers.MonoProviders.Base;
using Modules.Pooling.Core.Pool;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.View
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