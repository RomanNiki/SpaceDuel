using Model.Extensions.Pause;
using UnityEngine;

namespace Extensions.MappingUnityToModel
{
    public class PlayerAudioComponent : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private AudioSource _accelerationSource;
        [SerializeField] private AudioSource _rotateSource;
        [SerializeField] private AudioClip _rotateSound;
        [SerializeField] private AudioClip _moveSound;
        
        private void Pause()
        {
            _accelerationSource.Pause();
            _rotateSource.Pause();
        }

        private void UnPause()
        {
            _accelerationSource.UnPause();
            _rotateSource.UnPause();
        }

        public void PlayAccelerateSound()
        {
            if (_accelerationSource.isPlaying == false)
            { 
                _accelerationSource.PlayOneShot(_moveSound);
            }
        }

        public void StopAccelerateSound()
        {
            _accelerationSource.Stop();
        }

        public void PlayRotateSound()
        {
            if (_rotateSource.isPlaying == false)
                _rotateSource.PlayOneShot(_rotateSound);
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