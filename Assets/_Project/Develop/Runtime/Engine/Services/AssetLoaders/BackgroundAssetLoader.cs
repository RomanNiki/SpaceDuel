using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.Engine.Services.AssetLoaders
{
    public class BackgroundAssetLoader
    {
        private const string BackgroundPath = "Background1";
        private SceneInstance _sceneInstance;

        public async UniTask LoadBackgroundAsync()
        {
            var a = Addressables.LoadSceneAsync(BackgroundPath, LoadSceneMode.Additive);
            await a.Task;
            _sceneInstance = a.Result;
        }

        public async UniTask UnloadBackgroundAsync()
        {
            await Addressables.UnloadSceneAsync(_sceneInstance);
        }
    }
}