using Leopotam.Ecs;
using Model.Components.Events;
using Model.Unit.Input.Components.Events;

namespace Model.Unit.Input
{
    public sealed class InputPauseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputPauseQuitEvent> _inputPauseFilter;
        private readonly EcsFilter<GameRestartEvent> _restartEventFilter;
        private bool _isPause;

        public void Run()
        {
            if (_restartEventFilter.IsEmpty() == false)
                return;


            if (_inputPauseFilter.IsEmpty()) return;
            _isPause = !_isPause;
            _inputPauseFilter.GetEntity(0).Get<PauseEvent>().Pause = _isPause;
        }
    }
}