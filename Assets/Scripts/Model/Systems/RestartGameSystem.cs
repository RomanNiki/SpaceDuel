using System;
using Cysharp.Threading.Tasks;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Model.Systems
{
    public sealed class RestartGameSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<GameRestartRequest> _filterRequest;
        private readonly EcsFilter<GameRestartEvent> _filterEvent;
        [Inject] private Settings _settings;

        public void Run()
        {
            if (_filterEvent.GetEntitiesCount() > 0)
                return;  
            if (_filterRequest.GetEntitiesCount() <= 0)
                return; 
            OnRestartGame();
        }

        private async void OnRestartGame()
        {
            var restartEntity = _world.NewEntity();
            restartEntity.Get<GameRestartEvent>();
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
        }

        [Serializable]
        public class Settings
        {
            public float RestartDelay;
        }
    }
}