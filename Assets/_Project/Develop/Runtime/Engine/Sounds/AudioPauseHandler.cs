using System;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    public class AudioPauseHandler : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private GameAudioSource _audioSource;
        private IPauseService _pauseService;
        public event Action<bool> Pause;
        public bool Paused { get; private set; }
        
        [Inject]
        public void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
            _pauseService.AddPauseHandler(this);
        }

        private void OnDestroy()
        {
            _pauseService.RemovePauseHandler(this);
        }

        public void SetPaused(bool isPaused)
        {
            Pause?.Invoke(isPaused);
            Paused = isPaused;
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