using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Extensions.AssetLoaders
{
    public class LocalAssetLoader
    {
        private GameObject _cachedObject;

        protected async UniTask<T> LoadAndInstantiateInternal<T>(string assetId)
        {
            var handle = Addressables.InstantiateAsync(assetId);
            return await LoadHandler<T>(handle);
        }
        
        protected UniTask<T> LoadInternal<T>(string assetId)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(assetId);
            return LoadHandler<T>(handle);
        }

        private async UniTask<T> LoadHandler<T>(AsyncOperationHandle<GameObject> operation)
        {
            if (_cachedObject!= null)
            {
                Addressables.ReleaseInstance(_cachedObject);
            }
            var gameObject = await operation.Task;
            _cachedObject = gameObject;
            if (_cachedObject.TryGetComponent(out T component) == false)
                throw new NullReferenceException(
                    $"Object of type {typeof(T)} is null on attempt to load it from addresables"); 
             
            return component;
        }
        
        protected void UnloadInternal()
        {
            if (_cachedObject == null)
                return;
            var gameObject = _cachedObject.gameObject;
            gameObject.SetActive(false);
            Addressables.ReleaseInstance(gameObject);
            _cachedObject = null;
        }
    }
}