using System.Collections.Generic;
using Model.Extensions.Pause;
using Zenject;

namespace Extensions.Pause
{
    public sealed class PauseService : IPauseService
    {
        private readonly List<IPauseHandler> _pauseHandlers = new();
        private readonly TickableManager _tickableManager;
        public int Count => _pauseHandlers.Count;

        public PauseService(TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
        }

        public bool IsPause { get; private set; }

        public void SetPaused(bool isPaused)
        {
            IsPause = isPaused;
            _tickableManager.IsPaused = isPaused;
            foreach (var handler in _pauseHandlers)
            {
                handler.SetPaused(isPaused);
            }
        }

        public void AddPauseHandler(IPauseHandler pauseHandler)
        {
            _pauseHandlers.Add(pauseHandler);
        }

        public void RemovePauseHandler(IPauseHandler pauseHandler)
        {
            _pauseHandlers.Remove(pauseHandler);
        }
    }
}