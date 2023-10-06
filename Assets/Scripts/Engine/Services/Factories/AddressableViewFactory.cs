using Cysharp.Threading.Tasks;
using Modules.Pooling.Core.Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Engine.Services.Factories
{
    public class AddressableViewFactory<T> : IFactory<T>
        where T : Component
    {
        private readonly Vector3 _outGamePoint = new(-25, -25, -25);
        private readonly AssetReference _assetReference;

        public AddressableViewFactory(AssetReference assetReference)
        {
            _assetReference = assetReference;
        }

        public async UniTask<T> Create()
        {
            var item = await _assetReference.InstantiateAsync(_outGamePoint, Quaternion.identity).Task;
            return item.GetComponent<T>();
        }
        

        public void Dispose() => _assetReference.ReleaseAsset();
    }
}