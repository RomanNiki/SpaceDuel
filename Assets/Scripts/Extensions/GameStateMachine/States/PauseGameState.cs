using System.Collections.Generic;
using Extensions.AssetLoaders;
using Extensions.GameStateMachine.Transitions;
using Leopotam.Ecs;

namespace Extensions.GameStateMachine.States
{
    public class PauseGameState : State
    {
        private readonly PauseMenuProvider _provider;
        private readonly EcsWorld _world;
        
        public PauseGameState(EcsWorld world, PauseMenuProvider provider, List<Transition> transitions) : base(transitions)
        {
            _world = world;
            _provider = provider;
        }

        protected override async void OnEnter()
        {
           await _provider.Load(_world);
        }

        protected override void OnRun()
        {
        }

        public override void Exit()
        {
            _provider.Unload();
        }
    }
}