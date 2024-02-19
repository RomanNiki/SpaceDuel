using _Project.Develop.Runtime.Engine.Sounds.AudioStateMachine;
using _Project.Develop.Runtime.Engine.Sounds.Interfaces;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    public class MoveAudioSource : MonoBehaviour, IMoveAudioSource
    {
        [SerializeField] private AudioEngineStateMachine _engineStateMachine;
        [SerializeField] private OneShotWithDelayPlayer _rotateSoundPlayer;
        
        
        public void StartAcceleratingSound()
        {
            _engineStateMachine.SendStartRequestEngine();
        }

        public void StopAcceleratingSound()
        {
            _engineStateMachine.SendStopRequestEngine();
        }

        public void StartRotatingSound()
        {
            _rotateSoundPlayer.TryPlay().Forget();
        }

        public void StopRotatingSound()
        {
        }
    }
}