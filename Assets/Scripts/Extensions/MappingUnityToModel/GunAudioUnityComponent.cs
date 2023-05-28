using Model.Extensions.Pause;
using UnityEngine;

namespace Extensions.MappingUnityToModel
{
    public class GunAudioUnityComponent : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private AudioSource _audioSource;
        
        private AudioSource _audioSourceShot;

        private void Pause()
        {
            _audioSourceShot.Pause();
        }

        private void UnPause()
        {
            _audioSourceShot.UnPause();
        }
        
        public void PlayShoot(AudioClip clip) => _audioSource.PlayOneShot(clip);

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