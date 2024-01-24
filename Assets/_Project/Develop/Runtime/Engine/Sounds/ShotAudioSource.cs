using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    public class ShotAudioSource : MonoBehaviour
    {
        [SerializeField] private GameAudioSource _audioSource;

        public void ShotSound(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}