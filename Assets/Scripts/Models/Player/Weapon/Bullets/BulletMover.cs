using UnityEngine;
using Zenject;

namespace Models.Player.Weapon.Bullets
{
    public sealed class BulletMover : Mover, IFixedTickable
    {
        private readonly DamagerModel _damager;
        
        public BulletMover(DamagerModel damager, Camera camera) : base(damager, camera)
        {
            _damager = damager;
        }
        
        protected override void Rotate(float direction, float deltaTime)
        {
            _damager.Rotation = Mathf.Atan2(_damager.Velocity.y, _damager.Velocity.x) * Mathf.Rad2Deg + 90f;
        }

        public void FixedTick()
        {
           LoopedMove();
           Rotate(_damager.Velocity.x, Time.deltaTime);
        }
    }
}