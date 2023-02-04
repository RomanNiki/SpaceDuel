using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Extensions.GameStateMachine.Transitions;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.Extensions;
using Model.Extensions.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Extensions.GameStateMachine.States
{
    public class RestartGameState : State
    {
        private readonly EcsWorld _world;
        private readonly Settings _settings;
        private readonly IPauseService _pauseService;

        public RestartGameState(EcsWorld world, Settings settings, IPauseService pauseService, List<Transition> transitions) : base(transitions)
        {
            _world = world;
            _settings = settings;
            _pauseService = pauseService;
        }

        protected override async void OnEnter()
        {
            _pauseService.SetPaused(false);
            _world.SendMessage(new GameRestartEvent());

            await SlowDownTime();

            await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).ToUniTask().ContinueWith(() =>
            {
                Time.timeScale = 1f;
            });
        }

        protected override void OnRun()
        {
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

        public override void Exit()
        {
        }

        [Serializable]
        public class Settings
        {
            public float RestartDelay;
        }
    }
}