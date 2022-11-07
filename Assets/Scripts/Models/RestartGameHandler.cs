using System;
using Cysharp.Threading.Tasks;
using Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Models
{
    public class RestartGameHandler : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Settings _settings;

        public RestartGameHandler(Settings settings, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _settings = settings;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedMessage>(OnPlayerDied);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedMessage>(OnPlayerDied);
        }

        private async void OnPlayerDied(PlayerDiedMessage message)
        {
            await SlowDownTime();
            await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).ToUniTask().ContinueWith(() =>
            {
                Time.timeScale = 1f;
            });
        }

        private async UniTask SlowDownTime()
        {
            for (var t = 0f; t < _settings.RestartDelay; t += Time.deltaTime)
            {
                var normalizedTime = t / _settings.RestartDelay;
                Time.timeScale = Mathf.Lerp(1f, 0.5f, normalizedTime);
                await UniTask.Yield();
            }

            Time.timeScale = 0f;
        }

        [Serializable]
        public class Settings
        {
            public float RestartDelay;
        }
    }
}