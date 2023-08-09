using System;
using System.Collections.Generic;
using Model.Extensions.Pause;
using Zenject;

namespace Extensions.Pause
{
    public sealed class PauseRegisterHandler : IInitializable, IDisposable
    {
        private readonly List<IPauseHandler> _pauseHandlers;
        private readonly PauseService _pauseService;

        public PauseRegisterHandler(
            [Inject(Source = InjectSources.Local, Id = SystemsEnum.FixedRun)]
            List<IPauseHandler> pauseFixedRunHandlers,
            [Inject(Source = InjectSources.Local, Id = SystemsEnum.Run)]
            List<IPauseHandler> pauseRunHandlers,
            [Inject(Source = InjectSources.Local)] List<IPauseHandler> pauseHandlers, PauseService pauseService)
        {
            _pauseHandlers = new List<IPauseHandler>(pauseFixedRunHandlers);
            _pauseHandlers.AddRange(pauseRunHandlers);
            _pauseHandlers.AddRange(pauseHandlers);
            _pauseService = pauseService;
        }

        public void Initialize()
        {
            foreach (var pauseHandler in _pauseHandlers)
            {
                _pauseService.AddPauseHandler(pauseHandler);
            }
        }

        public void Dispose()
        {
            foreach (var pauseHandler in _pauseHandlers)
            {
                _pauseService.RemovePauseHandler(pauseHandler);
            }
        }
    }
}