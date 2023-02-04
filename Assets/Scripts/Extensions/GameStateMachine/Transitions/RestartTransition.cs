using Extensions.GameStateMachine.States;
using Leopotam.Ecs;
using Model.Components.Requests;

namespace Extensions.GameStateMachine.Transitions
{
    public class RestartTransition : Transition
    {
        private readonly EcsFilter<RestartGameRequest> _restartRequest;

        public RestartTransition(State targetState, EcsFilter<RestartGameRequest> restartRequest) : base(targetState)
        {
            _restartRequest = restartRequest;
        }

        public override void Tick()
        {
            if (_restartRequest.IsEmpty() == false)
            {
                foreach (var i in _restartRequest)
                {
                    _restartRequest.GetEntity(i).Destroy();
                }
                NeedTransit = true;
            }
        }
    }
}