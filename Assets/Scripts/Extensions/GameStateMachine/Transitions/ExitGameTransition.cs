using Extensions.GameStateMachine.States;
using Leopotam.Ecs;
using Model.Components.Requests;

namespace Extensions.GameStateMachine.Transitions
{
    public class ExitGameTransition : Transition
    {
        private readonly EcsFilter<ExitRequest> _exitFilter;

        public ExitGameTransition(State targetState, EcsFilter<ExitRequest> exitFilter) : base(targetState)
        {
            _exitFilter = exitFilter;
        }

        public override void Tick()
        {
            if (_exitFilter.IsEmpty() == false)
            {
                foreach (var i in _exitFilter)
                {
                    _exitFilter.GetEntity(i).Destroy();
                }
                NeedTransit = true;
            }
        }
    }
}