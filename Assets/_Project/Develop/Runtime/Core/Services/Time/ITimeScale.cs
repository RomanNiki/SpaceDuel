using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Core.Services.Time
{
    public interface ITimeScale : IResetable
    {
        float TimeScale { get; }
        UniTask SlowDown(float target, float duration = 0);
        UniTask Accelerate(float target, float duration = 0);
    }
}