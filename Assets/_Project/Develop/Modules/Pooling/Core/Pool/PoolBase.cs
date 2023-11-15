using System;
using System.Collections.Generic;
using _Project.Develop.Modules.Pooling.Core.Factory;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Modules.Pooling.Core.Pool
{
    public class PoolBase<TPoolObject> : IPool, ILoadingResource, IDisposable, ICleanup
    {
        private readonly List<TPoolObject> _activeItems;
        private readonly IFactory<TPoolObject> _factory;
        private readonly Stack<TPoolObject> _inactiveItems;
        private readonly Settings _settings;
        private int _currentSize;

        public PoolBase(Settings settings, IFactory<TPoolObject> factory)
        {
            _settings = settings;
            _factory = factory;
            _currentSize = 0;
            _inactiveItems = new Stack<TPoolObject>(_settings.InitialSize);
            _activeItems = new List<TPoolObject>(_settings.InitialSize);
        }

        public int ActiveCount { get; private set; }

        public void Cleanup()
        {
            var activeItemsTemp = new List<TPoolObject>(_activeItems);
            foreach (var activeItem in activeItemsTemp) OnActiveItemDispose(activeItem);

            _activeItems.Clear();
        }

        public void Dispose()
        {
            Cleanup();

            foreach (var inactiveItem in _inactiveItems) OnInactiveItemDispose(inactiveItem);

            _inactiveItems.Clear();
            OnDispose();
        }

        public async UniTask Load()
        {
            await LoadInternal();
        }

        void IPool.Despawn(object item)
        {
            Despawn((TPoolObject)item);
        }

        private TPoolObject CreateNew()
        {
            var item = _factory.Create();
            OnCreated(item);
            return item;
        }

        protected async UniTask LoadInternal()
        {
            if (_factory is ILoadingResource loadingResource) await loadingResource.Load();

            for (var i = 0; i < _settings.InitialSize; i++) _inactiveItems.Push(CreateNew());
        }

        protected TPoolObject GetInternal()
        {
            TPoolObject item;
            if (_inactiveItems.Count > 0)
            {
                item = _inactiveItems.Pop();
            }
            else
            {
                ExpandPool();
                item = CreateNew();
            }

            _activeItems.Add(item);
            ActiveCount++;
            OnSpawned(item);
            return item;
        }

        private void ExpandPool()
        {
            Resize(_currentSize * 2);
        }

        public void Resize(int desiredPoolSize)
        {
            if (_inactiveItems.Count == desiredPoolSize) return;

            while (_inactiveItems.Count > desiredPoolSize)
            {
                var item = _inactiveItems.Pop();
                OnDestroyed(item);
            }

            while (desiredPoolSize > _inactiveItems.Count) _inactiveItems.Push(CreateNew());

            _currentSize = desiredPoolSize;
        }

        public void Despawn(TPoolObject item)
        {
            ActiveCount--;
            _inactiveItems.Push(item);
            _activeItems.Remove(item);
            OnDespawned(item);
            if (_inactiveItems.Count > _settings.MaxSize) Resize(_settings.MaxSize);
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

        protected virtual void OnDispose()
        {
        }

        protected virtual void OnInactiveItemDispose(TPoolObject item)
        {
        }

        protected virtual void OnActiveItemDispose(TPoolObject item)
        {
        }

        public class Settings
        {
            public int InitialSize;
            public int MaxSize;

            public Settings(int initialSize, int maxSize = int.MaxValue - 1)
            {
                MaxSize = maxSize;
                InitialSize = initialSize;
            }

            public Settings()
            {
                MaxSize = int.MaxValue - 1;
                InitialSize = 0;
            }

            public static Settings Default => new(10, int.MaxValue);
        }
    }
}