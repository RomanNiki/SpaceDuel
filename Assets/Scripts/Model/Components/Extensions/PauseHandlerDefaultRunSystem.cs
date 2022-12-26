using Leopotam.Ecs;
using Model.Pause;

namespace Model.Components.Extensions
{
    public abstract class PauseHandlerDefaultRunSystem : IEcsRunSystem, IPauseHandler
    {
        private bool _pause;

        public void SetPaused(bool isPaused)
        {
            _pause = isPaused;
        }

        public void Run()
        {
            if (_pause)
                return;
            Tick();
        }

        protected abstract void Tick();
    }
}
