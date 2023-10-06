using System.Threading.Tasks;

namespace Core.Services
{
    public interface IGame
    {
        bool IsPlaying { get; }
        Task Start();
        Task Restart();
        void Stop();
    }
}