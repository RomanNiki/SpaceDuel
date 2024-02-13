using System;
using System.Threading;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Engine.Sounds.Ambient.Interfaces;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.ApplicationLifecycle.EntryPoints.Core
{
    public class CoreFlow : IAsyncStartable, IDisposable
    {
        private readonly IGame _game;
        private readonly IPauseService _pauseService;
        private readonly IAmbientSoundController _ambientSoundController;

        public CoreFlow(IGame game, IPauseService pauseService, IAmbientSoundController ambientSoundController)
        {
            _game = game;
            _pauseService = pauseService;
            _ambientSoundController = ambientSoundController;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _pauseService.AddPauseHandler(_game);
            _ambientSoundController.PlayGameAmbient();
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