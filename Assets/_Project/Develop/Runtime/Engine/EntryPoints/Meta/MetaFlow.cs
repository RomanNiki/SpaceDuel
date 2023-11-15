using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Engine.Services.AssetLoaders;
using _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations;
using _Project.Develop.Runtime.Engine.UI.Menu;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Meta
{
    public class MetaFlow : IStartable, IDisposable
    {
        private readonly IAssets _assets;
        private readonly LoadingScreenAssetLoader _loadingScreenAssetLoader;
        private readonly BackgroundAssetLoader _backgroundAssetLoader;
        private readonly Queue<ILoadingOperation> _loadingOperations = new();
        private readonly Menu _menu;
        private bool _isStarting;

        public MetaFlow(IAssets assets, LoadingScreenAssetLoader loadingScreenAssetLoader,
            BackgroundAssetLoader backgroundAssetLoader, Menu menu)
        {
            _assets = assets;
            _loadingScreenAssetLoader = loadingScreenAssetLoader;
            _backgroundAssetLoader = backgroundAssetLoader;
            _menu = menu;
        }

        public async void Start()
        {
            CreateLoadingQueue();
            await _backgroundAssetLoader.LoadBackgroundAsync();
            _menu.StartButtonClick += StartGame;
        }

        private Queue<ILoadingOperation> CreateLoadingQueue()
        {
            _loadingOperations.Clear();
            _loadingOperations.Enqueue(new LoadGameAssetsLoadingOperation(_assets));
            _loadingOperations.Enqueue(new UnloadSceneLoadingOperation(SceneManager.GetActiveScene()));
            _loadingOperations.Enqueue(new LoadGameLoadingOperation());
            return _loadingOperations;
        }

        private void StartGame()
        {
            if (_isStarting)
            {
                return;
            }
            _loadingScreenAssetLoader.LoadAndDestroy(_loadingOperations).Forget();
            _isStarting = true;
        }

        public void Dispose()
        {
            _isStarting = false;
            _menu.StartButtonClick -= StartGame;
        }
    }
}