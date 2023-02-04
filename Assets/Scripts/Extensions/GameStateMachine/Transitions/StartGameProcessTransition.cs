using Extensions.GameStateMachine.States;
using Leopotam.Ecs;
using Model.Components.Events;

namespace Extensions.GameStateMachine.Transitions
{
    public class StartGameProcessTransition : Transition
    {
        private readonly EcsFilter<GameStartedEvent> _startGameFilter;

        public StartGameProcessTransition(State targetState, EcsFilter<GameStartedEvent> startGameFilter) : base(
            targetState)
        {
            _startGameFilter = startGameFilter;
        }

        public override void Tick()
        {
            if (_startGameFilter.IsEmpty() == false)
            {
                foreach (var i in _startGameFilter)
                {
                    _startGameFilter.GetEntity(i).Destroy();
                }
                NeedTransit = true;
            }
        }
    }
}