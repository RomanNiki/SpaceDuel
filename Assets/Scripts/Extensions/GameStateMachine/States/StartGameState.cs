using System;
using System.Collections.Generic;
using System.Threading;
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

        protected override void OnEnter()
        {
            StartGame().Forget();
        }

        protected override void OnRun()
        {
        }

        public override void OnExit()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid StartGame()
        {
            try
            {
                var screen = await _provider.Load();
                _cancellationTokenSource = new CancellationTokenSource();
                await WaitStartAndDisappear(screen).AttachExternalCancellation(_cancellationTokenSource.Token);
                if (_world.IsAlive())
                {
                    _world.SendMessage(new GameStartedEvent());
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
                _provider.Unload();
            }
        }

        private async UniTask WaitStartAndDisappear(PrepareScreen screen)
        {
            await WaitStart(screen, _settings.SecondsToStart);
            await screen.Disappear();
        }

        private static async UniTask WaitStart(PrepareScreen text, float secondsToStart)
        {
            while (secondsToStart > 0f)
            {
                text.SetText(Mathf.CeilToInt(secondsToStart).ToString());
                secondsToStart -= Time.deltaTime;
                await UniTask.Yield();
            }
        }

        [Serializable]
        public class Settings
        {
            public float SecondsToStart;
        }
    }
}