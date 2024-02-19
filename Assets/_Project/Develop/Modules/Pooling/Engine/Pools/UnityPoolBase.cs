using System.Collections.Generic;
using _Project.Develop.Modules.Pooling.Core;
using _Project.Develop.Modules.Pooling.Core.Factory;
using _Project.Develop.Modules.Pooling.Core.Pool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Develop.Modules.Pooling.Engine.Pools
{
    public class UnityPoolBase<TPoolObject> : IPool<TPoolObject>, IFactory<TPoolObject>, ICleanup, ILoadingResource
        where TPoolObject : MonoBehaviour, IPoolItem
    {
        private readonly IFactory<TPoolObject> _factory;
        private readonly IObjectPool<TPoolObject> _objectPool;
        private readonly HashSet<TPoolObject> _activeObjects;

        public UnityPoolBase(IFactory<TPoolObject> factory, int defaultCapacity)
        {
            _factory = factory;
            _objectPool = new ObjectPool<TPoolObject>(CreateNew, OnGet, OnRelease, OnDestroyed, true, defaultCapacity);
            _activeObjects = new HashSet<TPoolObject>(defaultCapacity);
        }

        private void OnGet(TPoolObject obj)
        {
            _activeObjects.Add(obj);
        }

        private void OnRelease(TPoolObject obj)
        {
            _activeObjects.Remove(obj);
        }

        private TPoolObject CreateNew()
        {
            var item = _factory.Create();

            OnCreated(item);
            return item;
        }

        public void Despawn(TPoolObject item)
        {
            _objectPool.Release(item);
        }

        protected virtual void OnCreated(TPoolObject item)
        {
        }

        protected virtual void OnSpawned(TPoolObject item)
        {
        }

        protected virtual void OnDespawned(TPoolObject item)
        {
        }

        protected virtual void OnDestroyed(TPoolObject item)
        {
        }

        public TPoolObject Create()
        {
            return Spawn();
        }

        public void Dispose()
        {
            _objectPool.Clear();
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        public TPoolObject Spawn()
        {
            return _objectPool.Get();
        }

        public void Despawn(object obj)
        {
            Despawn((TPoolObject)obj);
        }

        public void Cleanup()
        {
            var copyActive = new HashSet<TPoolObject>(_activeObjects);
            foreach (var item in copyActive)
            {
                item.Dispose();
            }
        }

        public async UniTask Load()
        {
            if (_factory is ILoadingResource loadingResource)
            {
                await loadingResource.Load();
            }
        }
    }
}