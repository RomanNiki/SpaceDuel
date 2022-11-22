using System;
using System.Collections.Generic;
using Zenject;

namespace Pause
{
    public class PauseRegisterHandler: IInitializable, IDisposable
    {
        private readonly List<IPauseHandler> _pauseHandlers;
        private readonly PauseManager _pauseManager;

        public PauseRegisterHandler(
            [Inject(Source = InjectSources.Local)]
            List<IPauseHandler> pauseHandlers, PauseManager pauseManager)
        {
            _pauseHandlers = pauseHandlers;
            _pauseManager = pauseManager;
        }

        public void Initialize()
        {
            foreach (var pauseHandler in _pauseHandlers)
            {
                _pauseManager.AddPauseHandler(pauseHandler);
            }
        }

        public void Dispose()
        {
            foreach (var pauseHandler in _pauseHandlers)
            {
                _pauseManager.RemovePauseHandler(pauseHandler);
            }
        }
    }
}