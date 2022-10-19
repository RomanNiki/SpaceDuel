using UnityEngine;
using Zenject;

namespace Models.Player.Weapon.Bullets
{
    public class BulletMover : Mover, IFixedTickable
    {
        private BulletModel _bullet;
        
        public BulletMover(BulletModel bullet, Camera camera) : base(bullet, camera)
        {
            _bullet = bullet;
        }
        
        protected override void Rotate(float direction, float deltaTime)
        {
            _bullet.Rotation = direction;
        }

        public void FixedTick()
        {
           LoopedMove();
           
           Rotate(Vector3.Angle(_bullet.Position, _bullet.Position + _bullet.Velocity), Time.deltaTime);
        }
    }
}