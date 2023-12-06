using System.Threading;
using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Services.AssetLoaders;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Loading
{
    public class LoadingFlow : IAsyncStartable
    {
        private readonly BackgroundAssetLoader _backgroundAssetLoader;

        public LoadingFlow(BackgroundAssetLoader backgroundAssetLoader)
        {
            _backgroundAssetLoader = backgroundAssetLoader;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await SceneManager.LoadSceneAsync(Scenes.Meta);
            await _backgroundAssetLoader.LoadBackgroundAsync();
        }
    }
}