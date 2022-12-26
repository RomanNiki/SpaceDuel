using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Events.InputEvents;

namespace Model.Systems.Unit.Input
{
    public sealed class InputPauseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputPauseQuitEvent> _inputPauseFilter;
        private readonly EcsFilter<GameRestartEvent> _restartEvent;
        private bool _isPause;

        public void Run()
        {
            if (_restartEvent.IsEmpty() == false)
                return;
            

            if (_inputPauseFilter.IsEmpty() == false)
            {
                _isPause = !_isPause;
                _inputPauseFilter.GetEntity(0).Get<PauseEvent>().Pause = _isPause;
            }
        }
    }
}