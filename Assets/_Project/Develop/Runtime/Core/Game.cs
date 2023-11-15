using System;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Time;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Core
{
    public class Game : IGame
    {
        private readonly ISystemsController _systemsController;
        private readonly ITimeScale _timeScale;
        private readonly IUIFactory _uiFactory;
        private bool _isRestarting;
        
        public Game(ISystemsController systemsController, IUIFactory uiFactory, ITimeScale timeScale)
        {
            _systemsController = systemsController;
            _uiFactory = uiFactory;
            _timeScale = timeScale;
        }
        public bool IsPlaying { get; private set; }

        public void Start()
        {
            StartAsync().Forget();
        }

        private async UniTaskVoid StartAsync()
        {
            await _uiFactory.OpenControlsWindow(StartInternal);
        } 

        public void Restart()
        {
            if (_isRestarting == false)
            {
                RestartAsync().Forget();
                _isRestarting = true;
            }
        }

        private async UniTaskVoid RestartAsync()
        {
            try
            {
                await _timeScale.SlowDown(0.2f);
                IsPlaying = false;
                await UniTask.Yield();
                _systemsController.DisableSystems();
                await _uiFactory.OpenControlsWindow(StartInternal);
                await _timeScale.Accelerate(1f, 0f);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                _isRestarting = false;
            }
        }

        public void Stop()
        {
            IsPlaying = false;
            _systemsController?.DisableSystems();
        }

        private void StartInternal()
        {
            _uiFactory.CloseControlsWindow();
            _systemsController.EnableSystems();
            IsPlaying = true;
        }
    }
}