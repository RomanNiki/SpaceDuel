using System;
using Core.Input;
using Core.Services.Factories;
using Core.Services.Meta;
using Cysharp.Threading.Tasks;
using Engine.Services.AssetLoaders;
using UnityEngine;

namespace Engine.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly GameplayHudAssetLoader _gameplayHudAssetLoader;
        private readonly ControlsWindowAssetLoader _controlsWindowAssetLoader;
        private readonly IScore _score;
        private readonly IInput _input;

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