using Components;
using Enums;
using Extensions;
using Leopotam.Ecs;
using Models.Player.Weapon.Bullets;
using Presenters;
using Zenject;

namespace Models.Player.Weapon
{
    public sealed class BulletGun : DefaultGun
    {
        private readonly BulletPresenter.Factory _factory;
        private readonly Settings _settings;

        public BulletGun(ref EcsEntity weapon, Settings settings, [Inject(Id = BulletsEnum.Bullet)] BulletPresenter.Factory factory) : base(ref weapon)
        {
            _settings = settings;
            _factory = factory;
        }

        public override EcsEntity SpawnBullet()
        {
            var bullet = _factory.Create();
            ref var entity = ref bullet.GetProvider().Entity;
            return entity;
        }
    }
}