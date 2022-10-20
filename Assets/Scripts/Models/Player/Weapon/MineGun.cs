using System;
using UnityEngine;

namespace Models.Player.Weapon
{
    public class MineGun : DefaultGun
    {
        private readonly Settings _settings;
        
        public MineGun(AudioSource audioSource, PlayerModel playerModel, Settings settings) : base(audioSource, playerModel)
        {
            _settings = settings;
        }

        public override bool CanShoot()
        {
            return Time.realtimeSinceStartup - LastFireTime > _settings.MaxBombPlaceInterval;
        }
        
        protected override void PlaySound()
        {
            AudioSource.PlayOneShot(_settings.BombPlaceSound, _settings.BombPlaceSoundVolume);
        }

        protected override void InitBullet()
        {
            throw new NotImplementedException();
        }


        [Serializable]
        public class Settings
        {
            public AudioClip BombPlaceSound;
            public float BombPlaceSoundVolume;
            public float MaxBombPlaceInterval;
            public float BulletOffset;
        }
    }
}