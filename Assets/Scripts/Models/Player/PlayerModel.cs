using System;
using Messages;
using Models.Player.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Models.Player
{
    public class PlayerModel : Transformable, IDamageable
    {
        private readonly MeshRenderer _renderer;

        public PlayerModel(Settings settings, Rigidbody2D rigidBody,
            MeshRenderer renderer, Team team, IDyingPolicy dyingPolicy, SignalBus signalBus) : base(rigidBody)
        {
            Health = new ReactiveProperty<float>(settings.MaxHealth);
            Energy = new ReactiveProperty<float>(settings.MaxEnergy);
            _renderer = renderer;
            Team = team;
            Dead = Health.Select(dyingPolicy.Died).ToReactiveProperty();
            Dead.Where(x => x).Subscribe(_ => { signalBus.Fire(new PlayerDiedMessage() {Team = Team}); });
        }

        public ReactiveProperty<float> Health { get; private set; }
        public ReactiveProperty<float> Energy { get; private set; }
        public IReadOnlyReactiveProperty<bool> Dead { get; private set; }
        public Team Team { get; private set; }

        public MeshRenderer Renderer => _renderer;

        public void TakeDamage(float healthLoss)
        {
            Health.Value = Mathf.Max(0.0f, Health.Value - healthLoss);
        }

        public void SpendEnergy(float energyLoss)
        {
            Energy.Value = Mathf.Max(0.0f, Energy.Value - energyLoss);
        }

        [Serializable]
        public class Settings
        {
            public float MaxHealth;
            public float MaxEnergy;
        }
    }
}