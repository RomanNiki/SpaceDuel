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

        public Game(ISystemsController systemsController, IUIFactory uiFactory, ITimeScale timeScale)
        {
            _systemsController = systemsController;
            _uiFactory = uiFactory;
            _timeScale = timeScale;
        }
        
        public event Action Starting;
        public bool IsRestarting { get; private set; }
        public bool IsPlaying { get; private set; }

        public async UniTask Start()
        {
            Starting?.Invoke();
            await _uiFactory.OpenControlsWindow(StartInternal);
            await _timeScale.Accelerate(1f);
        }

        public void Restart()
        {
            if (IsRestarting == false)
                RestartAsync().Forget();
        }
        
        private async UniTaskVoid RestartAsync()
        {
            IsRestarting = true;
            
            try
            {
                await _timeScale.SlowDown(0.2f, 3f);
                IsPlaying = false;
                await UniTask.Yield();
                _systemsController.DisableSystems();
                await Start();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                IsRestarting = false;
            }
        }

        public void Stop()
        {
            IsPlaying = false;
            _systemsController.DisableSystems();
            _timeScale.Reset();
        }

        private void StartInternal()
        {
            _uiFactory.CloseControlsWindow();
            _systemsController.EnableSystems();
            IsPlaying = true;
        }

        private void Pause()
        {
            _timeScale.SetTimeScale(0f);
            _uiFactory.OpenPauseMenu().Forget();
        }

        private void UnPause()
        {
            _uiFactory.ClosePauseMenu();
            _timeScale.SetTimeScale(1f);
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }
}