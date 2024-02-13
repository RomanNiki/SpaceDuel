using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio
{
    public class GameAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SoundTypeEnum _soundType;
        [SerializeField] private bool _playOnAwake;
        [Range(0f, 1f)] [SerializeField] private float _volumeFactor = 1f;

        private readonly GameAudioMixer _gameAudioMixer = GameAudioMixer.Instance;
        public float VolumeFactor => _volumeFactor;

        public bool IsPlaying => _audioSource.isPlaying; 
        
        private void OnValidate()
        {
            if (_audioSource != null)
            {
                if (_audioSource.playOnAwake)
                {
                    _audioSource.playOnAwake = false;
                }
            }
        }

        private void OnEnable()
        {
            UpdateVolume();
            _gameAudioMixer.VolumeChanged += OnVolumeChanged;
            if (_playOnAwake)
            {
                Play();
            }
        }

        private void UpdateVolume()
        {
            _audioSource.volume = _gameAudioMixer.GetVolume(_soundType) * _volumeFactor;
        }

        private void OnVolumeChanged(SoundTypeEnum soundType)
        {
            if (soundType == _soundType)
            {
                UpdateVolume();
            }
        }

        private void OnDisable()
        {
            _gameAudioMixer.VolumeChanged -= OnVolumeChanged;
        }

        private float GetVolume(SoundTypeEnum soundType) =>
            _gameAudioMixer.GetVolume(soundType);

        public void SetVolumeFactor(float volumeFactor)
        {
            _volumeFactor = Mathf.Clamp01(volumeFactor);
            _audioSource.volume = _gameAudioMixer.GetVolume(_soundType) * _volumeFactor;
        }
        
        public void PlayOneShot(AudioClip clip)
        {
            PlayOneShot(clip, GetVolume(_soundType));
        }

        public void PlayOneShot(AudioClip clip, float volumeFactor)
        {
            _audioSource.PlayOneShot(clip, volumeFactor * GetVolume(_soundType));
        }

        public bool Loop
        {
            get => _audioSource.loop;
            set => _audioSource.loop = value;
        }

        public AudioClip Clip
        {
            get => _audioSource.clip;
            set => _audioSource.clip = value;
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public void Pause()
        {
            _audioSource.Pause();
        }

        public void UnPause()
        {
            _audioSource.UnPause();
        }
    }
}