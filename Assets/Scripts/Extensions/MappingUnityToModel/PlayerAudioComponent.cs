using Model.Extensions.Pause;
using UnityEngine;

namespace Extensions.MappingUnityToModel
{
    public class PlayerAudioComponent : MonoBehaviour, IPauseHandler
    {
        private AudioSource _audioAccelerateShot;
        private AudioSource _audioRotateShot;
        [SerializeField] private AudioClip _rotateSound;
        [SerializeField] private AudioClip _moveSound;

        private void Awake()
        {
            _audioAccelerateShot = gameObject.AddComponent<AudioSource>();
            _audioRotateShot = gameObject.AddComponent<AudioSource>();
        }

        private void Pause()
        {
            _audioAccelerateShot.Pause();
            _audioRotateShot.Pause();
        }

        private void UnPause()
        {
            _audioAccelerateShot.UnPause();
            _audioRotateShot.UnPause();
        }

        public void PlayAccelerateSound(bool accelerate)
        {
            if (accelerate == false)
            {
                _audioAccelerateShot.Stop();
                return;
            }

            if (_audioAccelerateShot.isPlaying == false)
            {
                _audioAccelerateShot.PlayOneShot(_moveSound, 0.25f);
            }
        }

        public void PlayRotateSound()
        {
            if (_audioRotateShot.isPlaying == false)
                _audioRotateShot.PlayOneShot(_rotateSound, 0.25f);
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused)
            {
                Pause();
                return;
            }

            UnPause();
        }
    }
}