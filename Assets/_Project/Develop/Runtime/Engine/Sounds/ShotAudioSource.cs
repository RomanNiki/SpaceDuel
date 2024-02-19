using System;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using _Project.Develop.Runtime.Engine.Sounds.Interfaces;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    public class ShotAudioSource : MonoBehaviour, ISoundAction
    {
        [SerializeField] private GameAudioSource _audioSource;

        public void ShotSound(AudioClip audioClip)
        {
            Play?.Invoke();
            _audioSource.PlayOneShot(audioClip);
        }

        public event Action Play;
    }
}