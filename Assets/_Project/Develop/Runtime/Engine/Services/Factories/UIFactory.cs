using System;
using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;
using _Project.Develop.Runtime.Engine.Services.AssetLoaders;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly ControlsWindowAssetLoader _controlsWindowAssetLoader;
        private readonly GameplayHudAssetLoader _gameplayHudAssetLoader;
        private readonly IInput _input;
        private readonly IScore _score;

        public UIFactory(GameplayHudAssetLoader gameplayHudAssetLoader,
            ControlsWindowAssetLoader controlsWindowAssetLoader, IScore score, IInput input)
        {
            _gameplayHudAssetLoader = gameplayHudAssetLoader;
            _controlsWindowAssetLoader = controlsWindowAssetLoader;
            _score = score;
            _input = input;
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

        public async UniTask OpenControlsWindow(Action startGameAction)
        {
            try
            {
                var controls = await _controlsWindowAssetLoader.LoadAndInstantiate();
                controls.Constuct(startGameAction, _input);
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
            _controlsWindowAssetLoader.DestroyAndUnload();
        }
    }
}