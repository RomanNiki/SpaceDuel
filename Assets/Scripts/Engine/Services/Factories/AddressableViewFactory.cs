using Cysharp.Threading.Tasks;
using Modules.Pooling.Core;
using Modules.Pooling.Core.Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Engine.Services.Factories
{
    public class AddressableViewFactory<T> : IFactory<T>, ILoadingResource
        where T : Component
    {
        private readonly Vector3 _outGamePoint = new(-25, -25, -25);
        private readonly AssetReference _assetReference;
        private GameObject _prefab;

        public AddressableViewFactory(AssetReference assetReference)
        {
            _assetReference = assetReference;
        }

        public async UniTask Load()
        {
            _prefab = await _assetReference.LoadAssetAsync<GameObject>().Task;
        }

        T IFactory<T>.Create()
        {
            var item = Object.Instantiate(_prefab, _outGamePoint, Quaternion.identity);
            return item.GetComponent<T>();
        }

        public void Dispose() => _assetReference.ReleaseAsset();
    }
}