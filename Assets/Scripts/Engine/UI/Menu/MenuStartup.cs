using System.Collections.Generic;
using Core.Services;
using Cysharp.Threading.Tasks;
using Engine.Services.AssetLoaders;
using Engine.Services.Loading.LoadingOperations;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Engine.UI.Menu
{
    public class MenuStartup : MonoBehaviour
    {
        private IAssets _assets;
        private LifetimeScope _currentScope;
        private LoadingScreenAssetLoader _loadingScreenAssetLoader;

        [Inject]
        public void Construct(IAssets assets, LifetimeScope currentScope,
            LoadingScreenAssetLoader loadingScreenAssetLoader)
        {
            _assets = assets;
            _currentScope = currentScope;
            _loadingScreenAssetLoader = loadingScreenAssetLoader;
        }

        private void Start()
        {
            LoadGame(CreateLoadingQueue()).Forget();
        }

        private Queue<ILoadingOperation> CreateLoadingQueue()
        {
            Queue<ILoadingOperation> loadingOperations = new();
            loadingOperations.Enqueue(new LoadGameAssetsLoadingOperation(_assets));
            loadingOperations.Enqueue(new LoadGameLoadingOperation(_currentScope));
            loadingOperations.Enqueue(new UnloadSceneLoadingOperation(SceneManager.GetActiveScene()));
            return loadingOperations;
        }

        private async UniTaskVoid LoadGame(Queue<ILoadingOperation> loadingOperations)
        {
            await _loadingScreenAssetLoader.LoadAndDestroy(loadingOperations);
        }
    }
}