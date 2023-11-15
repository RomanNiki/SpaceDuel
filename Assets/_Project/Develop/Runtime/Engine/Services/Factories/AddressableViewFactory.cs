using _Project.Develop.Modules.Pooling.Core;
using _Project.Develop.Modules.Pooling.Core.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Develop.Runtime.Engine.Services.Factories
{
    public class AddressableViewFactory<T> : IFactory<T>, ILoadingResource
        where T : Component
    {
        private readonly AssetReference _assetReference;
        private readonly Vector3 _outGamePoint = new(-25, -25, -25);
        private GameObject _prefab;

        public AddressableViewFactory(AssetReference assetReference)
        {
            _assetReference = assetReference;
        }

        T IFactory<T>.Create()
        {
            var item = Object.Instantiate(_prefab, _outGamePoint, Quaternion.identity);
            return item.GetComponent<T>();
        }

        public void Dispose() => _assetReference.ReleaseAsset();

        public async UniTask Load()
        {
            _prefab = await _assetReference.LoadAssetAsync<GameObject>().Task;
        }
    }
}