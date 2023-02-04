using Extensions.GameStateMachine.States;

namespace Extensions.GameStateMachine.Transitions
{
    public abstract class Transition
    {
        private readonly State _targetState;

        public Transition(State targetState)
        {
            _targetState = targetState;
        }

        public State TargetState => _targetState;
        public bool NeedTransit { get; protected set; }


        public abstract void Tick();
        
        public void Init()
        {
            NeedTransit = false;
        }
    }
}