using System;
using Models.Player.Interfaces;
using UnityEngine;

namespace Models.Player
{
    public class DamageHandler : IDamageable
    {
        private readonly AudioSource _audioPlayer;
        private readonly Settings _settings;
        private readonly IDamageable _damageable;
        
        public DamageHandler(IDamageable damageable, Settings settings, AudioSource audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _settings = settings;
            _damageable = damageable;
        }

        public void TakeDamage(float damage)
        {
            _audioPlayer.PlayOneShot(_settings.HitSound, _settings.HitSoundVolume);
            _damageable.TakeDamage(damage);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip HitSound;
            public float HitSoundVolume = 1.0f;
        }
    }
}