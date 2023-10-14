using Cysharp.Threading.Tasks;

namespace Core.Services
{
    public interface IGame
    {
        bool IsPlaying { get; }
        UniTask Start();
        UniTask Restart();
        void Stop();
    }
}