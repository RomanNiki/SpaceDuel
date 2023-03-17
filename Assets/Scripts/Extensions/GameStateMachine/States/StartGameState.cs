using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Extensions.AssetLoaders;
using Extensions.GameStateMachine.Transitions;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.Extensions;
using UnityEngine;
using Views;

namespace Extensions.GameStateMachine.States
{
    public class StartGameState : State
    {
        private readonly Settings _settings;
        private readonly PrepareGameScreenProvider _provider;
        private readonly EcsWorld _world;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _token;

        public StartGameState(Settings settings, PrepareGameScreenProvider provider,
            EcsWorld world, List<Transition> transitions) : base(transitions)
        {
            _world = world;
            _settings = settings;
            _provider = provider;
        }

        protected override async void OnEnter()
        {
            CheckTokenSource();
            _cancellationTokenSource = new CancellationTokenSource();
            _token = _cancellationTokenSource.Token;
            var prepareScreen = await _provider.Load();
            await WaitToStart(prepareScreen, _settings.SecondsToStart, _token);
        }

        protected override void OnRun()
        {
        }
        
        public override void OnExit()
        {
            CheckTokenSource();
        }

        private void CheckTokenSource()
        {
            if (_cancellationTokenSource == null) return;
            _cancellationTokenSource.Cancel();
            _provider.Unload();
        }

        private async UniTask WaitToStart(PrepareScreen text, float secondsToStart, CancellationToken token)
        {
            while (secondsToStart > 0f)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                text.SetText(Mathf.CeilToInt(secondsToStart).ToString());
                secondsToStart -= Time.deltaTime;
                await UniTask.Yield();
            }

            await Unload(text);
            if (_world.IsAlive())
            {
                _world.SendMessage(new GameStartedEvent());
            }
        }

        private async Task Unload(PrepareScreen text)
        {
            await text.Disappear();
            _provider.Unload();
            _cancellationTokenSource = null;
        }
        

        [Serializable]
        public class Settings
        {
            public float SecondsToStart;
        }
    }
}