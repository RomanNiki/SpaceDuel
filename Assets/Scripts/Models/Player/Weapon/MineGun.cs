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
            return Time.realtimeSinceStartup - LastFireTime > _settings.MaxShootInterval;
        }
        
        protected override void PlaySound()
        {
            AudioSource.PlayOneShot(_settings.ShootSound, _settings.ShootSoundVolume);
        }

        protected override void InitBullet()
        {
            throw new NotImplementedException();
        }
    }
}