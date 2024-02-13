using _Project.Develop.Runtime.Core.Services.Pause;

namespace _Project.Develop.Runtime.Engine.Sounds.Ambient.Interfaces
{
    public interface IAmbientSoundController : IPauseHandler
    {
        void PlayMenuAmbient();
        void PlayGameAmbient();
    }
}