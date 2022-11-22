using Components;
using Enums;
using Extensions;
using Leopotam.Ecs;
using Models.Player.Weapon.Bullets;
using Presenters;
using Zenject;

namespace Models.Player.Weapon
{
    public sealed class MineGun : DefaultGun
    {
        private readonly BulletPresenter.Factory _factory;
        
        public MineGun(ref EcsEntity weapon, [Inject(Id = BulletsEnum.Mine)] BulletPresenter.Factory factory) : base(ref weapon)
        {
            _factory = factory;
        }
        
        public override EcsEntity SpawnBullet()
        {
            var mine =  _factory.Create();
            return mine.GetProvider().Entity;
        }
    }
}