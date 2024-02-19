using System;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using _Project.Develop.Runtime.Engine.Sounds.AudioStateMachine.States;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds.AudioStateMachine
{
    public class AudioEngineStateMachine : MonoBehaviour
    {
        [SerializeField] private GameAudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private float _targetVolumeFactor = 0.7f;
        [Range(0, 1)] [SerializeField] private float _fadeTime = 0.5f;

        private State _currentState;
        private IdleState _idleState;
        private PlayState _playState;
        private StoppingState _stoppingState;
        protected virtual bool IsPaused => false;

        private void Awake()
        {
            _idleState = new IdleState(_audioSource, _audioClip);
            _playState = new PlayState(_audioSource, _targetVolumeFactor, _fadeTime);
            _stoppingState = new StoppingState(_audioSource, _fadeTime);
        }

        private void OnEnable()
        {
            _audioSource.SetVolumeFactor(0f);
            _currentState = _idleState;
        }

        public void SendStartRequestEngine()
        {
            ChangeState(_playState);
        }

        public void SendStopRequestEngine()
        {
            ChangeState(_stoppingState);
        }

        private void ChangeState(State state)
        {
#if DEBUG
            if (state == null)
            {
                throw new ArgumentNullException($"State cannot be null");
            }
#endif
            if (state.GetType().Name == _currentState.GetType().Name)
            {
                return;
            }

            _currentState?.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }

        private void Update()
        {
            if (IsPaused)
            {
                return;
            }

            _currentState?.OnUpdate();

            if (_currentState is null or IdleState)
            {
                return;
            }

            if (Mathf.Approximately(_audioSource.VolumeFactor, 0f) && _currentState is StoppingState)
            {
                ChangeState(new IdleState(_audioSource, _audioClip));
            }
        }
    }
}