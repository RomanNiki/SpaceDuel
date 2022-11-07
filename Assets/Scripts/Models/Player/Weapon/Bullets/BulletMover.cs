using UnityEngine;

namespace Models.Player.Weapon.Bullets
{
    public sealed class BulletMover : Mover
    {
        private readonly DamagerModel _damager;
        
        public BulletMover(DamagerModel damager, Camera camera) : base(damager, camera)
        {
            _damager = damager;
        }
        
        protected override void Rotate()
        {
            _damager.Rotation = Mathf.Atan2(_damager.Velocity.y, _damager.Velocity.x) * Mathf.Rad2Deg + 90f;
        }

        protected override void Move()
        {
        }

        protected override bool CanMove() => true;
    }
}