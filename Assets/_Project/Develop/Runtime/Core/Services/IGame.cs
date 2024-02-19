using _Project.Develop.Runtime.Core.Services.Pause;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Core.Services
{
    public interface IGame : IPauseHandler
    {
        bool IsPlaying { get; }
        bool IsRestarting { get; }
        UniTask Start();
        UniTaskVoid Restart();
        void Stop();
    }
}