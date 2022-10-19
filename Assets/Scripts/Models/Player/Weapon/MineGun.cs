using UnityEngine;

namespace Models.Player.Weapon
{
    public class MineGun : DefaultGun
    {
        public MineGun(AudioSource audioSource, Settings settings) : base(audioSource, settings)
        {
        }

        public override bool CanShoot()
        {
            return base.CanShoot();
        }

        public override void Shoot()
        {
            base.Shoot();
        }
    }
}