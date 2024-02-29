using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Core.Services.Pause.Services
{
    public class PauseService : IPauseService
    {
        private readonly ICollection<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();
        public bool IsPause { get; private set; }
        public event Action<bool> PauseStateChanged;

        public void SetPaused(bool isPaused)
        {
            IsPause = isPaused;
            foreach (var pauseHandler in _pauseHandlers)
            {
                pauseHandler.SetPaused(isPaused);
            }

            PauseStateChanged?.Invoke(isPaused);
        }

        public void AddPauseHandler(IPauseHandler pauseHandler) => _pauseHandlers.Add(pauseHandler);
        
        public void RemovePauseHandler(IPauseHandler pauseHandler) => _pauseHandlers.Remove(pauseHandler);
        
        public void Reset()
        {
            IsPause = false;
        }
    }
}