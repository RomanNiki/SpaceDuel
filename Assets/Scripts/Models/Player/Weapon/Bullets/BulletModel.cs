using System;
using UnityEngine;

namespace Models.Player.Weapon.Bullets
{
    public class BulletModel : Transformable
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Settings _settings;

        public BulletModel(Rigidbody2D rb, Settings settings) : base(rb)
        {
            _settings = settings;
            _rigidbody2D = rb;
            Damage = _settings.Damage;
            Energy = _settings.StartEnergy;
        }
        
        public float Damage { get; private set; }
        public float Energy { get; private set; }
        public Action EnergyEnded;
        
        public void SpendEnergy(float count)
        {
            Energy = Mathf.Max(0.0f, Energy - count);
            if (Energy == 0f)
            {
                EnergyEnded?.Invoke();
            }
        }

        public void Reset()
        {
            Energy = _settings.StartEnergy;
            _rigidbody2D.velocity = Vector2.zero;
        }
        
        [Serializable]
        public class Settings
        {
            public float StartEnergy;
            public float Damage;
        }
    }
}