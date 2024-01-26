using System.Threading;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using _Project.Develop.Runtime.Engine.Sounds.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    public class MoveAudioSource : MonoBehaviour, IMoveAudioSource, IPauseHandler
    {
        [SerializeField] private AudioClip _loopAcceleratingAudioClip;
        [SerializeField] private AudioClip _rotateClip;
        [Range(0f, 1f)] [SerializeField] private float _rotationVolumeFactor = 0.4f;
        [SerializeField] private GameAudioSource _audioSource;
        [Range(0f, 1f)] [SerializeField] private float _accelerationMaxVolumeFactor = 0.7f;

        private bool _isEngineWorking;
        private bool _isStopRequest;
        private bool _isRotateSoundPlay;
        private const float RotationSoundDelay = 0.4f;
        private CancellationTokenSource _disableTokenSource;
        private IPauseService _pauseService;
        private bool _isPaused;
        
        [Inject]
        public void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
            _pauseService.AddPauseHandler(this);
        }

        private void OnEnable()
        {
            _disableTokenSource ??= new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _disableTokenSource?.Cancel();
            _disableTokenSource?.Dispose();
            _disableTokenSource = null;
            _isEngineWorking = false;
            _isRotateSoundPlay = false;
        }

        private void OnDestroy()
        {
            _pauseService.RemovePauseHandler(this);
        }

        public void StartAcceleratingSound()
        {
            _isStopRequest = false;
            if (_isEngineWorking) return;
            _isEngineWorking = true;
            PlayAcceleratingSoundLoop(_disableTokenSource.Token).Forget();
        }

        public void StopAcceleratingSound()
        {
            _isStopRequest = true;
        }

        public void StartRotatingSound()
        {
            PlayOneShotRotatingSound();
        }

        public void StopRotatingSound()
        {
        }

        public void PlayOneShotRotatingSound()
        {
            if (_isRotateSoundPlay) return;
            _audioSource.PlayOneShot(_rotateClip, _rotationVolumeFactor);
            _isRotateSoundPlay = true;
            WaitEndOfRotationSound(_rotateClip.length + RotationSoundDelay, _disableTokenSource.Token).Forget();
        }

        private async UniTaskVoid WaitEndOfRotationSound(float length, CancellationToken token = default)
        {
            await UniTask.WaitForSeconds(length, cancellationToken: token);
            _isRotateSoundPlay = false;
        }

        private async UniTaskVoid PlayAcceleratingSoundLoop(CancellationToken token = default)
        {
            try
            {
                _audioSource.SetVolumeFactor(0f);
                _audioSource.Loop = true;
                _audioSource.Clip = _loopAcceleratingAudioClip;
                _audioSource.Play();
                
                while (_isEngineWorking)
                {
                    if (_isPaused == false)
                    {
                        if (_isStopRequest)
                        {
                            _audioSource.SetVolumeFactor(Mathf.Lerp(_audioSource.VolumeFactor, 0f, Time.deltaTime));
                            if (Mathf.Approximately(_audioSource.VolumeFactor, 0f))
                            {
                                _isEngineWorking = false;
                            }
                        }
                        else if (_audioSource.VolumeFactor < _accelerationMaxVolumeFactor)
                        {
                            _audioSource.SetVolumeFactor(Mathf.Lerp(_audioSource.VolumeFactor,
                                _accelerationMaxVolumeFactor,
                                Time.deltaTime));
                        }
                    }

                    await UniTask.Yield();
                }
            }
            catch when (token.IsCancellationRequested)
            {
            }
            finally
            {
                if (_audioSource != null)
                {
                    _audioSource.Loop = false;
                    _audioSource.Stop();
                }
            }
        }

        public async UniTask SetPaused(bool isPaused)
        {
            _isPaused = isPaused;

            if (isPaused)
            {
                _audioSource.Pause();
            }
            else
            {
                _audioSource.UnPause();
            }
        }
    }
}