using System.Threading.Tasks;

namespace Core.Services.Time
{
    public interface ITimeScale
    {
        float TimeScale { get; }
        Task SlowDown(float target, float duration = 3f);
        Task Accelerate(float target, float duration = 3f);
    }
}