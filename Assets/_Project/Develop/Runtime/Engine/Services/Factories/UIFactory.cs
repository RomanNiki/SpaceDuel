using System;
using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;
using _Project.Develop.Runtime.Engine.Services.AssetLoaders;
using _Project.Develop.Runtime.Engine.UI.Controls;
using _Project.Develop.Runtime.Engine.UI.Menu.Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly ControlsWindowAssetLoader _controlsWindowAssetLoader;
        private readonly GameplayHudAssetLoader _gameplayHudAssetLoader;
        private readonly PauseMenuAssetLoader _pauseMenuAssetLoader;
        private readonly IAssets _assets;
        private readonly IInput _input;
        private readonly IScore _score;
        private readonly LoadingScreenAssetLoader _loadingScreenAssetLoader;
        private ControlsController _controlsController;
        private PauseMenuController _pauseMenuController;


        public UIFactory(GameplayHudAssetLoader gameplayHudAssetLoader,
            ControlsWindowAssetLoader controlsWindowAssetLoader, IScore score, IInput input,
            PauseMenuAssetLoader pauseMenuAssetLoader, LoadingScreenAssetLoader loadingScreenAssetLoader, IAssets assets)
        {
            _gameplayHudAssetLoader = gameplayHudAssetLoader;
            _controlsWindowAssetLoader = controlsWindowAssetLoader;
            _score = score;
            _input = input;
            _pauseMenuAssetLoader = pauseMenuAssetLoader;
            _loadingScreenAssetLoader = loadingScreenAssetLoader;
            _assets = assets;
        }

        public async UniTask OpenGameplayHud()
        {
            try
            {
                var hud = await _gameplayHudAssetLoader.LoadAndInstantiate();
                hud.Construct(_score);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public async UniTask OpenPauseMenu()
        {
            try
            {
                var view = await _pauseMenuAssetLoader.LoadAndInstantiate();
                _pauseMenuController =
                    new PauseMenuController(view, _assets, _loadingScreenAssetLoader);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public async UniTask OpenControlsWindow(Action startGameAction)
        {
            try
            {
                var controlsView = await _controlsWindowAssetLoader.LoadAndInstantiate();
                _controlsController = new ControlsController(startGameAction, _input, controlsView);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void CloseGameplayHud()
        {
            _gameplayHudAssetLoader.DestroyAndUnload();
        }

        public void CloseControlsWindow()
        {
            _controlsController?.Dispose();
            _controlsWindowAssetLoader.DestroyAndUnload();
        }

        public void ClosePauseMenu()
        {
            _pauseMenuController?.Dispose();
            _pauseMenuAssetLoader.DestroyAndUnload();
        }
    }
}