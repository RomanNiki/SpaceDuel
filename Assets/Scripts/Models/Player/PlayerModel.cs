using System;
using Messages;
using Models.Player.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Models.Player
{
    public sealed class PlayerModel : Transformable, IDamageable, IEnergyContainer
    {
        private readonly ReactiveProperty<float> _energy;
        private readonly ReactiveProperty<float> _health;
        private readonly Settings _settings;

        public PlayerModel(Settings settings, Rigidbody2D rigidBody, Team team, IDyingPolicy dyingPolicy, SignalBus signalBus) : base(rigidBody)
        {
            _health = new ReactiveProperty<float>(settings.MaxHealth);
            _energy = new ReactiveProperty<float>(settings.MaxEnergy);
            _settings = settings;
            Team = team;
            Dead = Health.Select(dyingPolicy.Died).ToReactiveProperty();
            Dead.Where(x => x).Subscribe(_ => { signalBus.Fire(new PlayerDiedMessage {Team = Team}); });
        }

        public IReadOnlyReactiveProperty<float> Health => _health;
        public IReadOnlyReactiveProperty<float> Energy => _energy;
        public IReadOnlyReactiveProperty<bool> Dead { get; }
        public Team Team { get; }

        public void TakeDamage(float healthLoss)
        {
            _health.Value = Mathf.Max(0.0f, Health.Value - healthLoss);
        }
        
        public void SpendEnergy(float energyLoss)
        {
            _energy.Value = Mathf.Max(0.0f, Energy.Value - energyLoss);
        }
        
        public void ChargeEnergy(float energyLoss)
        {
            _energy.Value = Mathf.Min(_settings.MaxEnergy, Energy.Value + energyLoss);
        }

        [Serializable]
        public class Settings
        {
            public float MaxHealth;
            public float MaxEnergy;
        }
    }
}