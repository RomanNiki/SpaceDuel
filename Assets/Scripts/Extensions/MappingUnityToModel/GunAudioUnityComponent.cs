using Model.Pause;
using UnityEngine;

namespace Extensions.MappingUnityToModel
{
    public class GunAudioUnityComponent : MonoBehaviour, IPauseHandler
    {
        private AudioSource _audioSourceShot;
        
        private void Awake()
        {
            _audioSourceShot = gameObject.AddComponent<AudioSource>();
        }

        private void Pause()
        {
            _audioSourceShot.Pause();
        }

        private void UnPause()
        {
            _audioSourceShot.UnPause();
        }
        
        public void PlayShoot(AudioClip clip) => _audioSourceShot.PlayOneShot(clip, 0.25f);

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