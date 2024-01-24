using System;
using System.Threading;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Pause;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.ApplicationLifecycle.EntryPoints.Core
{
    public class CoreFlow : IAsyncStartable, IDisposable
    {
        private readonly IGame _game;
        private readonly IPauseService _pauseService;

        public CoreFlow(IGame game, IPauseService pauseService)
        {
            _game = game;
            _pauseService = pauseService;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _pauseService.AddPauseHandler(_game);
            await _game.Start().AttachExternalCancellation(cancellation);
        }

        public void Dispose()
        {
            _pauseService.RemovePauseHandler(_game);
            _pauseService.Reset();
            _game.Stop();
        }
    }
}