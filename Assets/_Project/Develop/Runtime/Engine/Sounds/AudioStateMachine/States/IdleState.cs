using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds.AudioStateMachine.States
{
    public class IdleState : State
    {
        private readonly AudioClip _audioClip;

        public IdleState(GameAudioSource audioSource, AudioClip audioClip) : base(audioSource)
        {
            _audioClip = audioClip;
        }

        public override void OnEnter()
        {
            AudioSource.Stop();
        }

        public override void OnExit()
        {
            AudioSource.Loop = true;
            AudioSource.Clip = _audioClip;
            AudioSource.Play();
        }
    }
}