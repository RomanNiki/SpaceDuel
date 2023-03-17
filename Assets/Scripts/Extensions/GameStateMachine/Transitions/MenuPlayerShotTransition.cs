using Extensions.GameStateMachine.States;
using Leopotam.Ecs;
using Model.Unit.Input.Components.Events;

namespace Extensions.GameStateMachine.Transitions
{
    public class MenuPlayerShotTransition : Transition
    {
        private readonly EcsFilter<InputShootStartedEvent> _anyKeyFilter;

        public MenuPlayerShotTransition(State targetState, EcsFilter<InputShootStartedEvent> anyKeyFilter) : base(targetState)
        {
            _anyKeyFilter = anyKeyFilter;
        }

        public override void Tick()
        {
            if (_anyKeyFilter.IsEmpty() == false)
            {
                NeedTransit = true;
            }
        }
    }
}