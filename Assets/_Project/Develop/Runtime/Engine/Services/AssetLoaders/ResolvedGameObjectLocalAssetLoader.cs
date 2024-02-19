using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.Services.AssetLoaders
{
    public class ResolvedGameObjectLocalAssetLoader : LocalAssetLoader
    {
        private readonly IObjectResolver _resolver;

        public ResolvedGameObjectLocalAssetLoader(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        protected async UniTask<T> LoadAndInstantiateWithInjection<T>(string assetId)
            where T : MonoBehaviour
        {
            var obj = await LoadAndInstantiateInternal<T>(assetId);
            _resolver.InjectGameObject(obj.gameObject);
            return obj;
        }
    }
}