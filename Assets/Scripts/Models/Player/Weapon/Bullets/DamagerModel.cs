using System;
using Models.Player.Interfaces;
using UniRx;
using UnityEngine;

namespace Models.Player.Weapon.Bullets
{
    public class DamagerModel : Transformable, IEnergyContainer
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Settings _settings;
        private readonly ReactiveProperty<float> _energy;
        
        public DamagerModel(Rigidbody2D rb, Settings settings) : base(rb)
        {
            _settings = settings;
            _rigidbody2D = rb;
            Damage = _settings.Damage;
            _energy = new ReactiveProperty<float>(_settings.StartEnergy);
            _rigidbody2D.position = Vector2.up * 40;
        }

        public float Damage { get; }
        public IReadOnlyReactiveProperty<float> Energy => _energy;
        public Action EnergyEnded;

        public void SpendEnergy(float count)
        {
            _energy.Value = Mathf.Max(0.0f, Energy.Value - count);
            if (Energy.Value == 0f)
            {
                EnergyEnded?.Invoke();
            }
        }

        public void ChargeEnergy(float count)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            _energy.Value = _settings.StartEnergy;
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