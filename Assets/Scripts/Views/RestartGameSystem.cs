using System;
using Cysharp.Threading.Tasks;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Extensions;
using Model.Unit.Movement.Components.Tags;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Views
{
    public sealed class RestartGameSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<GameRestartRequest> _filterRequest;
        private readonly EcsFilter<GameRestartEvent> _filterEvent;
        private readonly EcsFilter<ViewObjectComponent>.Exclude<PlayerTag> _filter;
        [Inject] private Settings _settings;

        protected override void Tick()
        {
            if (_filterEvent.IsEmpty() == false)
                return;
            if (_filterRequest.IsEmpty())
                return;
            OnRestartGame();
        }

        private async void OnRestartGame()
        {
            _world.SendMessage(new GameRestartEvent());

            await SlowDownTime();

            await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).ToUniTask().ContinueWith(() =>
            {
                Time.timeScale = 1f;
            });
        }

        private async UniTask SlowDownTime()
        {
            for (var t = 0f; t < _settings.RestartDelay; t += Time.unscaledDeltaTime)
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