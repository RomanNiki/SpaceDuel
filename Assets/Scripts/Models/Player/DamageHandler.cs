using System;
using Models.Player.Interfaces;
using UnityEngine;

namespace Models.Player
{
    public class DamageHandler : IDamageable
    {
        private readonly AudioSource _audioPlayer;
        private readonly Settings _settings;
        private readonly PlayerModel _player;


        public DamageHandler(PlayerModel player, Settings settings, AudioSource audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _settings = settings;
            _player = player;
        }

        public void TakeDamage(float damage)
        {
            _audioPlayer.PlayOneShot(_settings.HitSound, _settings.HitSoundVolume);
            _player.TakeDamage(damage);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip HitSound;
            public float HitSoundVolume = 1.0f;
        }
    }
}