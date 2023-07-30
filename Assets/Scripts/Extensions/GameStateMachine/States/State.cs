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

        public void Enter()
        {
            foreach (var transition in _transitions)
            {
                transition.Init();
            }

            OnEnter();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnRun()
        {
        }

        public void Run()
        {
            foreach (var transition in _transitions)
            {
                transition.Tick();
            }

            OnRun();
        }

        public virtual void OnExit()
        {
        }

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