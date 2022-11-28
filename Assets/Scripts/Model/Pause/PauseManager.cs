using System.Collections.Generic;
using Zenject;

namespace Model.Pause
{
    public class PauseManager
    {
        private readonly List<IPauseHandler> _pauseHandlers = new();
        private readonly TickableManager _tickableManager;
        public int Count => _pauseHandlers.Count;

        public PauseManager(TickableManager tickableManager)
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