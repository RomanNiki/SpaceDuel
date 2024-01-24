using _Project.Develop.Modules.Pooling.Core;
using _Project.Develop.Modules.Pooling.Core.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.Services.Factories
{
    public class AddressableViewFactory<T> : IFactory<T>, ILoadingResource
        where T : Component
    {
        private readonly AssetReference _assetReference;
        private readonly IObjectResolver _resolver;
        private readonly Vector3 _outGamePoint = new(-25, -25, -25);
        private GameObject _prefab;

        public AddressableViewFactory(AssetReference assetReference, IObjectResolver resolver)
        {
            _assetReference = assetReference;
            _resolver = resolver;
        }

        T IFactory<T>.Create()
        {
            var item = _resolver.Instantiate(_prefab, _outGamePoint, Quaternion.identity);
            return item.GetComponent<T>();
        }

        public void Dispose() => _assetReference.ReleaseAsset();

        public async UniTask Load()
        {
            _prefab = await _assetReference.LoadAssetAsync<GameObject>().Task;
        }
    }
}