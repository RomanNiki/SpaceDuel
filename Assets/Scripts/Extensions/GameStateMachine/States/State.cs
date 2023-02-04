using System.Collections.Generic;
using Extensions.GameStateMachine.Transitions;
using UnityEngine;

namespace Extensions.GameStateMachine.States
{
    public abstract class State
    {
        private readonly List<Transition> _transitions;

        public State(List<Transition> transitions)
        {
            _transitions = transitions;
        }

        public State()
        {
            
        }
        
        public void Enter()
        {
            foreach (var transition in _transitions)
            {
                transition.Init();
            }
            OnEnter();
        }

        protected abstract void OnEnter();
        
        protected abstract void OnRun();

        public void Run()
        {
            foreach (var transition in _transitions)
            {
                transition.Tick();
            }
            OnRun();
        }

        public abstract void Exit();

        public State GetNextState()
        {
           
            foreach (var transition in _transitions)
            {
                if (transition.NeedTransit)
                    return transition.TargetState;
            }

            return null;
        }
    }
}