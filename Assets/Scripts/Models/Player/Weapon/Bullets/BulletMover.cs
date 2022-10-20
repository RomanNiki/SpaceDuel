using UnityEngine;
using Zenject;

namespace Models.Player.Weapon.Bullets
{
    public class BulletMover : Mover, IFixedTickable
    {
        private readonly BulletModel _bullet;
        
        public BulletMover(BulletModel bullet, Camera camera) : base(bullet, camera)
        {
            _bullet = bullet;
        }
        
        protected override void Rotate(float direction, float deltaTime)
        {
            _bullet.Rotation = Mathf.Atan2(_bullet.Velocity.y, _bullet.Velocity.x) * Mathf.Rad2Deg + 90f;
        }

        public void FixedTick()
        {
           LoopedMove();
           Rotate(_bullet.Velocity.x, Time.deltaTime);
        }
    }
}