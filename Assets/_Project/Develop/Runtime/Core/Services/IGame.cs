namespace _Project.Develop.Runtime.Core.Services
{
    public interface IGame
    {
        bool IsPlaying { get; }
        void Start();
        void Restart();
        void Stop();
    }
}