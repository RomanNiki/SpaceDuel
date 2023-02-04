using System.Collections.Generic;
using Extensions.GameStateMachine.Transitions;
using Model.Extensions.Pause;

namespace Extensions.GameStateMachine.States
{
    public class GameProcessState : State
    {
        private readonly IPauseService _pauseService;

        public GameProcessState(IPauseService pauseService, List<Transition> transitions) : base(transitions)
        {
            _pauseService = pauseService;
        }

        protected override void OnEnter()
        {
            _pauseService?.SetPaused(false);
        }

        protected override void OnRun()
        {
        }

        public override void Exit()
        {
            _pauseService?.SetPaused(true);
        }
    }
}