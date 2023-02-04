using Extensions.GameStateMachine.States;
using Leopotam.Ecs;
using Model.Components.Events;

namespace Extensions.GameStateMachine.Transitions
{
    public class PauseTransition : Transition
    {
        private readonly EcsFilter<PauseRequest> _pauseFilter;

        public PauseTransition(State targetState, EcsFilter<PauseRequest> pauseFilter) : base(targetState)
        {
            _pauseFilter = pauseFilter;
        }
        

        public override void Tick()
        {
            if (_pauseFilter.IsEmpty() == false)
            {
                foreach (var i in _pauseFilter)
                {
                    _pauseFilter.GetEntity(i).Destroy();
                }
                NeedTransit = true;
            }
        }
    }
}