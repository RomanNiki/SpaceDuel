using System.Collections.Generic;
using Extensions.AssetLoaders;
using Extensions.GameStateMachine.Transitions;

namespace Extensions.GameStateMachine.States
{
    public class ControlsState : State
    {
        private readonly ControlsScreenProvider _controlsScreenProvider;

        public ControlsState(ControlsScreenProvider controlsScreenProvider, List<Transition> transitions) : base (transitions)
        {
            _controlsScreenProvider = controlsScreenProvider;
        }
        
        protected override async void OnEnter()
        {
            await _controlsScreenProvider.Load();
        }

        protected override void OnRun()
        {
        }

        public override void Exit()
        {
            _controlsScreenProvider.Unload();
        }
    }
}