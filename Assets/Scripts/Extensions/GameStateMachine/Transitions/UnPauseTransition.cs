using Extensions.GameStateMachine.States;
using Leopotam.Ecs;
using Model.Components.Requests;

namespace Extensions.GameStateMachine.Transitions
{
    public class UnPauseTransition : Transition
    {
        private readonly EcsFilter<StartGameRequest> _unpauseFilter;

        public UnPauseTransition(State targetState, EcsFilter<StartGameRequest> unpauseFilter) : base(targetState)
        {
            _unpauseFilter = unpauseFilter;
        }

        public override void Tick()
        {
            if (_unpauseFilter.IsEmpty() == false)
            {
                foreach (var i in _unpauseFilter)
                {
                    _unpauseFilter.GetEntity(i).Destroy();
                }
                NeedTransit = true;
            }
        }
    }
}