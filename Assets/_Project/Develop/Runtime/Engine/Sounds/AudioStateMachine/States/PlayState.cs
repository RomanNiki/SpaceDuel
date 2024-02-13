using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds.AudioStateMachine.States
{
    public class PlayState : State
    {
        private readonly float _targetVolumeFactor;
        private readonly float _fadeTime;
        private float _elapsedTime;

        public PlayState(GameAudioSource audioSource, float targetVolumeFactor, float fadeTime) : base(audioSource)
        {
            _targetVolumeFactor = targetVolumeFactor;
            _fadeTime = fadeTime;
        }

        public override void OnEnter()
        {
            _elapsedTime = 0f;
        }

        public override void OnUpdate()
        {
            if (AudioSource.VolumeFactor < _targetVolumeFactor || Mathf.Approximately(_elapsedTime, _fadeTime))
            {
                AudioSource.SetVolumeFactor(Mathf.Lerp(AudioSource.VolumeFactor,
                    _targetVolumeFactor,
                    _elapsedTime / _fadeTime));
                _elapsedTime += Time.deltaTime;
            }
        }
    }
}