using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Core.Input.Components;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Engine.Services.AssetLoaders;
using _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations;
using Scellecs.Morpeh;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.Engine.UI.Menu.Controllers
{
    public class PauseMenuController : IDisposable
    {
        private readonly IGame _game;
        private readonly IPauseService _pauseService;
        private readonly PauseMenuView _pauseMenuView;
        private readonly IAssets _assets;
        private readonly LoadingScreenAssetLoader _loadingScreenAssetLoader;
        private readonly Queue<ILoadingOperation> _loadingOperations = new();

        public PauseMenuController(PauseMenuView pauseMenuView, IAssets assets,
            LoadingScreenAssetLoader loadingScreenAssetLoader)
        {
            _pauseMenuView = pauseMenuView;
            _assets = assets;
            _loadingScreenAssetLoader = loadingScreenAssetLoader;
            pauseMenuView.StartButtonClick += StartGame;
            pauseMenuView.ExitButtonClick += OnExit;
            CreateLoadingQueue();
        }

        public void Dispose()
        {
            _pauseMenuView.StartButtonClick -= StartGame;
            _pauseMenuView.ExitButtonClick -= OnExit;
            _loadingOperations.Clear();
        }

        private void StartGame()
        {
            World.Default.SendMessage(new InputMenuEvent());
        }

        private void OnExit()
        {
            _loadingScreenAssetLoader.LoadAndDestroy(_loadingOperations).Forget();
        }

        private void CreateLoadingQueue()
        {
            _loadingOperations.Clear();
            var activeScene = SceneManager.GetActiveScene();
            _loadingOperations.Enqueue(new UnloadGameAssetsLoadingOperation(_assets));
            _loadingOperations.Enqueue(new UnloadSceneLoadingOperation(activeScene));
            _loadingOperations.Enqueue(new LoadMenuLoadingOperation());
        }
    }
}