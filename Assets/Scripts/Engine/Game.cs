using System;
using Core.Services;
using Core.Services.Factories;
using Core.Services.Time;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh.Addons.Feature.Unity;
using UnityEngine;

namespace Engine
{
    public class Game : IGame
    {
        private readonly BaseFeaturesInstaller _featuresInstaller;
        private readonly IUIFactory _uiFactory;
        private readonly ITimeScale _timeScale;

        public Game(BaseFeaturesInstaller featuresInstaller, IUIFactory uiFactory, ITimeScale timeScale)
        {
            _featuresInstaller = featuresInstaller;
            _uiFactory = uiFactory;
            _timeScale = timeScale;
        }

        public bool IsPlaying { get; private set; }

        public async UniTask Start()
        {
            await _uiFactory.OpenControlsWindow(StartInternal);
        }

        public async UniTask Restart()
        {
            try
            {
                await _timeScale.SlowDown(0.2f);
                IsPlaying = false;
                await UniTask.Yield();
                _featuresInstaller.gameObject.SetActive(false);
                await _uiFactory.OpenControlsWindow(StartInternal);
                await _timeScale.Accelerate(1, 1f);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void StartInternal()
        {
            _uiFactory.CloseControlsWindow();
            _featuresInstaller.gameObject.SetActive(true);
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            if (_featuresInstaller == null) return;
            _featuresInstaller.gameObject.SetActive(false);
        }
    }
}