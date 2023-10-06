using System;
using System.Threading.Tasks;
using Core.Services.Factories;
using Core.Services.Meta;
using Engine.Services.AssetLoaders;
using UnityEngine;

namespace Engine.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly GameplayHudAssetLoader _gameplayHudAssetLoader;
        private readonly ControlsWindowAssetLoader _controlsWindowAssetLoader;
        private readonly IScore _score;

        public UIFactory(GameplayHudAssetLoader gameplayHudAssetLoader,
            ControlsWindowAssetLoader controlsWindowAssetLoader, IScore score)
        {
            _gameplayHudAssetLoader = gameplayHudAssetLoader;
            _controlsWindowAssetLoader = controlsWindowAssetLoader;
            _score = score;
        }

        public async Task OpenGameplayHud()
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

        public async Task OpenControlsWindow(Action startGameAction)
        {
            try
            {
                var controls = await _controlsWindowAssetLoader.LoadAndInstantiate();
                controls.Constuct(startGameAction);
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