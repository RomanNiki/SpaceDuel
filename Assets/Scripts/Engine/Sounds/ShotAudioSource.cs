using UnityEngine;

namespace Engine.Sounds
{
    public class ShotAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void ShotSound(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}