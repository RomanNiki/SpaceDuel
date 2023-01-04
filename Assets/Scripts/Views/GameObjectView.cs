using Model.Components.Extensions.Interfaces.Pool;
using UnityEngine;
using Zenject;

namespace Views
{
    public class GameObjectView : MonoBehaviour, IPoolable<IMemoryPool>, IPoolObject
    {
        public Transform Transform { get; private set; }
        private IMemoryPool _memoryPool;

        protected virtual void Awake()
        {
            Transform = transform;
        }

        public void OnDespawned()
        {
            _memoryPool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _memoryPool = pool;
        }

        public void PoolRecycle()
        {
            _memoryPool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<GameObjectView>
        {
        }
    }
}