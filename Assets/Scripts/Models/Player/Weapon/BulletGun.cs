using System;
using Presenters;
using UnityEngine;

namespace Models.Player.Weapon
{
    public class BulletGun : DefaultGun
    {
        private readonly BulletPresenter.Factory _factory;
        private readonly Settings _settings;
        
        public BulletGun(AudioSource audioSource, Settings settings, PlayerModel playerModel, BulletPresenter.Factory factory) : base(audioSource, playerModel)
        {
            _factory = factory;
            _settings = settings;
        }

        public override bool CanShoot()
        {
            return Time.realtimeSinceStartup - LastFireTime > _settings.MaxShootInterval;
        }

        protected override void PlaySound()
        {
            AudioSource.PlayOneShot(_settings.ShootSound, _settings.ShootSoundVolume);
        }

        protected override void InitBullet()
        {
            var spawnPosition = PlayerModel.Position + PlayerModel.LookDir * _settings.BulletOffset;
            _factory.Create(_settings.StartBulletSpeed, spawnPosition, PlayerModel.LookDir);
        }
        
        [Serializable]
        public class Settings
        {
            public AudioClip ShootSound;
            public float ShootSoundVolume;
            public float StartBulletSpeed;
            public float MaxShootInterval;
            public float BulletOffset;
        }
    }
}