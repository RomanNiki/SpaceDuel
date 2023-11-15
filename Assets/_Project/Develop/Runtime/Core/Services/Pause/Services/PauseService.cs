using System.Collections.Generic;

namespace _Project.Develop.Runtime.Core.Services.Pause.Services
{
    public class PauseService : IPauseService
    {
        private ICollection<IPauseHandler> _pauseHandlers;
        public bool IsPause { get; private set; }

        public PauseService(ICollection<IPauseHandler> pauseHandlers)
        {
            _pauseHandlers = pauseHandlers;
        }

        public PauseService()
        {
            _pauseHandlers = new List<IPauseHandler>();
        }

        public void SetPaused(bool isPaused)
        {
            IsPause = isPaused;
            foreach (var pauseHandler in _pauseHandlers)
            {
                pauseHandler.SetPaused(isPaused);
            }
        }

        public void AddPauseHandler(IPauseHandler pauseHandler) => _pauseHandlers.Add(pauseHandler);


        public void RemovePauseHandler(IPauseHandler pauseHandler) => _pauseHandlers.Remove(pauseHandler);
    }
}