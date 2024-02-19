using System;
using _Project.Develop.Runtime.Engine.Sounds.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    public class PlayPitchChanger : IDisposable
    {
        private ISoundAction _shotAudioSource;
        private AudioSource _audioSource;

        public PlayPitchChanger(ISoundAction shotAudioSource, AudioSource audioSource)
        {
            _shotAudioSource = shotAudioSource;
            _audioSource = audioSource;
            _shotAudioSource.Play += OnShot;
        }
        
        private void OnShot()
        {
            var pitch = Random.Range(0.9f, 1.2f);
            _audioSource.pitch = pitch;
        }

        public void Dispose()
        {
            _shotAudioSource.Play -= OnShot;
        }
    }
}