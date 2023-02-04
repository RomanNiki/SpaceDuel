using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
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
            if (_isPause)
            {
                _inputPauseFilter.GetEntity(0).Get<PauseRequest>();
            }
            else
            {
                _inputPauseFilter.GetEntity(0).Get<StartGameRequest>();
            }
        }
    }
}