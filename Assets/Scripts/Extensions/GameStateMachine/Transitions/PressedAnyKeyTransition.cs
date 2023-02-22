using Extensions.GameStateMachine.States;
using Leopotam.Ecs;
using Model.Unit.Input.Components.Events;

namespace Extensions.GameStateMachine.Transitions
{
    public class PressedAnyKeyTransition : Transition
    {
        private readonly EcsFilter<InputAnyKeyEvent> _anyKeyFilter;

        public PressedAnyKeyTransition(State targetState, EcsFilter<InputAnyKeyEvent> anyKeyFilter) : base(targetState)
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