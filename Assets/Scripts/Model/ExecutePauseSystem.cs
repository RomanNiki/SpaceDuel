using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Extensions.Pause;
using Model.Unit.Destroy.Components.Requests;
using Zenject;

namespace Model
{
    public sealed class ExecutePauseSystem : IEcsRunSystem
    {
        private readonly IPauseService _pauseService;
        private readonly EcsFilter<PauseEvent> _pauseFilter;
        private readonly EcsFilter<StartGameRequest> _startFilter;

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
            {
                _pauseService?.SetPaused(true);
            }

            if (_startFilter.IsEmpty()) return;
            _pauseService?.SetPaused(false);
            foreach (var i in _startFilter)
            {
                _startFilter.GetEntity(i).Get<EntityDestroyRequest>();
            }
        }
    }
}