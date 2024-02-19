using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Core.Services.Pause
{
    public interface IPauseHandler
    {
        UniTask SetPaused(bool isPaused);
    }
}