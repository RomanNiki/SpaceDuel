using Cysharp.Threading.Tasks;
using Modules.Pooling.Core.Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Modules.Pooling.Engine.Factories
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

        public T Create(Vector3 position = new(), float rotation = 0)
        {
            while (_assetReference.IsDone == false)
            {
                UniTask.Yield();
            }


            var item = Object.Instantiate(_prefab, position, Quaternion.Euler(0, 0, rotation), _parent);
            return item;
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