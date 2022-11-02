using Presenters;
using UnityEngine;

namespace Models.Player.Weapon
{
    public sealed class MineGun : DefaultGun
    {
        private readonly Settings _settings;
        private readonly MinePresenter.Factory _factory;
        
        public MineGun(AudioSource audioSource, PlayerModel playerModel, Settings settings, MinePresenter.Factory factory) : base(audioSource, playerModel)
        {
            _settings = settings;
            _factory = factory;
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
            var spawnPosition = PlayerModel.Position + PlayerModel.LookDir * _settings.SpawnOffset;
            _factory.Create(spawnPosition);
            PlayerModel.SpendEnergy(_settings.EnergyCost);
        }
    }
}