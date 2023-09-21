using Cysharp.Threading.Tasks;
using Modules.Pooling.Core.Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Engine.Factories
{
    public class AddressableViewFactory<T> : IFactory<T>
        where T : Component
    {
        private T _prefab;
        private readonly Transform _parent;
        private readonly AssetReference _assetReference;

        public AddressableViewFactory(AssetReference assetReference, Transform parent = null)
        {
            _assetReference = assetReference;
            _parent = parent;
        }

        public T Create()
        {
            while (_assetReference.IsDone == false)
            {
                UniTask.Yield();
            }
            
            return Object.Instantiate(_prefab, _parent);
        }

        public async UniTask Load()
        {
            var handle = _assetReference.LoadAssetAsync<GameObject>();
            while (handle.IsDone == false && handle.Status != AsyncOperationStatus.Failed)
            {
                await UniTask.Yield();
            }

            _prefab = handle.Result.GetComponent<T>();
        }

        public void Dispose() =>
            _assetReference.ReleaseAsset();
    }
}