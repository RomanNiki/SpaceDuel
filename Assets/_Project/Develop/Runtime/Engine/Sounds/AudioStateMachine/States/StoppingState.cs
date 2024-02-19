using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds.AudioStateMachine.States
{
    public class StoppingState : State
    {
        private readonly float _fadeTime;
        private float _elapsedTime;

        public StoppingState(GameAudioSource audioSource, float fadeTime) : base(audioSource)
        {
            _fadeTime = fadeTime;
        }

        public override void OnEnter()
        {
            _elapsedTime = 0f;
        }

        public override void OnUpdate()
        {
            if (Mathf.Approximately(AudioSource.VolumeFactor, 0f)) return;
            AudioSource.SetVolumeFactor(Mathf.Lerp(AudioSource.VolumeFactor, 0f, _elapsedTime / _fadeTime));
            _elapsedTime += Time.deltaTime;
        }
    }
}