using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

namespace Extensions.Factories
{
    public sealed class AddressableFactory<T> : IFactory<T>,IInitializable, IDisposable
    {
        private GameObject _prefab;
        private readonly DiContainer _container;
        private readonly AssetReference _assetReference;
        private bool _done;
        private readonly Stack<GameObject> _spawned = new();

        public AddressableFactory(DiContainer diContainer, AssetReference reference)
        {
            _container = diContainer;
            _assetReference = reference;
        }

        public T Create()
        {
            var obj =  _container.InstantiatePrefab(_prefab);
            _spawned.Push(obj);
            return obj.GetComponent<T>();
        }

        public void Dispose()
        {
            foreach (var item in _spawned)
            {
                Object.Destroy(item);
            }
            _assetReference.ReleaseAsset();
        }

        public async void Initialize()
        {
            var handle = _assetReference.LoadAssetAsync<GameObject>().Task;
            _prefab = await handle;
        }
    }
}