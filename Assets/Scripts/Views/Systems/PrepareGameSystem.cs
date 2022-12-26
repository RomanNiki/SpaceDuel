using System;
using System.Threading;
using System.Threading.Tasks;
using Extensions.AssetLoaders;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Extensions;
using UnityEngine;
using Zenject;

namespace Views.Systems
{
    public class PrepareGameSystem : IEcsRunSystem, IEcsInitSystem
    {
        [Inject] private Settings _settings;
        [Inject] private PrepareGameScreenProvider _provider;
        private EcsFilter<PauseEvent> _filter;
        private EcsFilter<StartGameEvent> _startFilter;
        private EcsWorld _world;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _token;

        public void Init()
        {
            _world.SendMessage(new PauseEvent() {Pause = false});
        }

        public async void Run()
        {
            if (_filter.IsEmpty() == false)
            {
                CheckTokenSource();
                if (_filter.Get1(0).Pause == false)
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    _token = _cancellationTokenSource.Token;
                    var text = await _provider.Load();
                    await WaitToStart(text, _settings.SecondsToStart, _token);
                }
            }

            if (_startFilter.IsEmpty()) return;
            CheckTokenSource();
        }

        private void CheckTokenSource()
        {
            if (_cancellationTokenSource == null) return;
            _provider.Unload();
            _cancellationTokenSource.Cancel();
        }

        private async Task WaitToStart(PrepareScreen text, float secondsToStart, CancellationToken token)
        {
            while (secondsToStart > 0f)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                text.SetText(Mathf.CeilToInt(secondsToStart).ToString());
                secondsToStart -= Time.deltaTime;
                await Task.Yield();
            }

            _provider.Unload();
            _cancellationTokenSource = null;
            _world.SendMessage(new StartGameEvent());
        }


        [Serializable]
        public class Settings
        {
            public float SecondsToStart;
        }
    }
}