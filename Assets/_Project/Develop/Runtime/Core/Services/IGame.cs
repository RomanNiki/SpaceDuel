using System;
using _Project.Develop.Runtime.Core.Services.Pause;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Core.Services
{
    public interface IGame : IPauseHandler
    {
        event Action Starting;
        bool IsPlaying { get; }
        bool IsRestarting { get; }
        UniTask Start();
        void Restart();
        void Stop();
    }
}