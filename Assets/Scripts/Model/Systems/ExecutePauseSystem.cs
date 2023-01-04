using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Pause;
using Zenject;

namespace Model.Systems
{
    public sealed class ExecutePauseSystem : IEcsRunSystem
    {
        [Inject] private PauseService _pauseService;
        private EcsFilter<PauseEvent> _pause;
        private EcsFilter<StartGameRequest> _start;

        public void Run()
        {
            if (_pause.IsEmpty() == false)
            {
                _pauseService.SetPaused(true);
            }

            if (_start.IsEmpty()) return;
            _pauseService.SetPaused(false);
            foreach (var i in _start)
            {
                _start.GetEntity(i).Get<EntityDestroyRequest>();
            }
        }
    }
}