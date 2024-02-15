using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio
{
    public class GameAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SoundTypeEnum _soundType;
        [SerializeField] private bool _playOnAwake;
        [field: SerializeField, Range(0f, 1f)] public float VolumeFactor { get; private set; } = 1f;

        [SerializeField] private GameAudioMixer _gameAudioMixer;
        private bool _isGameAudioMixerNotNull;
        public bool IsPlaying => _audioSource.isPlaying;

        private void Awake()
        {
            _isGameAudioMixerNotNull = _gameAudioMixer != null;
        }

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
            if (_gameAudioMixer != null)
            {
                _gameAudioMixer.VolumeChanged += OnVolumeChanged;
            }

            if (_playOnAwake)
            {
                Play();
            }
        }

        private void UpdateVolume()
        {
            if (_gameAudioMixer != null)
            {
                _audioSource.volume = _gameAudioMixer.GetVolume(_soundType) * VolumeFactor;
            }
            else
            {
                _audioSource.volume = VolumeFactor;
            }
        }

        private void OnVolumeChanged(SoundTypeEnum soundType)
        {
            UpdateVolume();
        }

        private void OnDisable()
        {
            if (_gameAudioMixer != null)
            {
                _gameAudioMixer.VolumeChanged -= OnVolumeChanged;
            }
        }

        private float GetVolume(SoundTypeEnum soundType)
        {
            if (_gameAudioMixer != null)
            {
                return _gameAudioMixer.GetVolume(soundType) * VolumeFactor;
            }

            return VolumeFactor;
        }


        public void SetVolumeFactor(float volumeFactor)
        {
            VolumeFactor = Mathf.Clamp01(volumeFactor);
            var volume = VolumeFactor;
            if (_isGameAudioMixerNotNull)
            {
                volume *= _gameAudioMixer.GetVolume(_soundType);
            }
            _audioSource.volume = volume;
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