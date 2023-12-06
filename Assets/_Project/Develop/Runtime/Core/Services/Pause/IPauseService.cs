using System;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Core.Services.Pause
{
    public interface IPauseService
    {
        public event Action<bool> PauseStateChanged;
        public bool IsPause { get; }

        public UniTask SetPaused(bool isPaused);

        public void AddPauseHandler(IPauseHandler pauseHandler);

        public void RemovePauseHandler(IPauseHandler pauseHandler);
    }
}