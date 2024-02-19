using _Project.Develop.Runtime.Engine.Infrastructure.Audio;

namespace _Project.Develop.Runtime.Engine.Sounds.AudioStateMachine.States
{
    public class State
    {
        protected readonly GameAudioSource AudioSource;

        public State(GameAudioSource audioSource)
        {
            AudioSource = audioSource;
        }
        
        public virtual void OnEnter()
        {
            
        }
        
        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
    }
}