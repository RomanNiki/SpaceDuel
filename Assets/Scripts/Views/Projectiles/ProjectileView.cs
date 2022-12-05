using Controller.EntityToGameObject;
using Model.Components.Extensions.Pool;
using UnityEngine;
using Zenject;

namespace Views.Projectiles
{
    [RequireComponent(typeof(EcsUnityProvider))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ProjectileView<T> : EcsUnityNotifier, IPoolable<IMemoryPool>, IPhysicsPoolObject
    {
        private IMemoryPool _memoryPool;
        public Rigidbody2D Rigidbody2D { get; private set; }
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
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

        public class Factory : PlaceholderFactory<T>
        {
        }
    }
}