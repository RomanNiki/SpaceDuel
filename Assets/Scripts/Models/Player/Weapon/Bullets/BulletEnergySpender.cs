using UnityEngine;
using Zenject;

namespace Models.Player.Weapon.Bullets
{
    public class BulletEnergySpender : ITickable
    {
        private readonly BulletModel _bullet;

        public BulletEnergySpender(BulletModel bullet)
        {
            _bullet = bullet;
        }
        
        public void Tick()
        {
            if (_bullet.Energy > 0f)
            {
                _bullet.SpendEnergy(Time.deltaTime);
            }
        }
    }
}