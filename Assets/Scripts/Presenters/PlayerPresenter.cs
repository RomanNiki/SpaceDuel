﻿using Models.Player;
using Models.Player.Interfaces;
using Models.Player.Weapon.Bullets;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class PlayerPresenter : MonoBehaviour, IDamageVisitor
    {
        [Inject] private PlayerModel _playerModel;
        [Inject] private DamageHandler _damageHandler;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent<IDamageVisitor>(out var player))
            {
                var health = player.Health();
                player.Visit(_playerModel);
                TakeDamage(health);
            }
        }

        private void TakeDamage(float value)
        {
            _damageHandler.TakeDamage(value);
        }
        
        public IReadOnlyReactiveProperty<float> HealthProperty => _playerModel.Health;
        public IReadOnlyReactiveProperty<float> EnergyProperty => _playerModel.Energy;
        public Vector3 Position => _playerModel.Position;
        public float Health() => HealthProperty.Value;

        public void Visit(BulletModel bullet)
        {
            TakeDamage(bullet.Damage);
        }

        public void Visit(PlayerModel playerModel)
        {
            TakeDamage(playerModel.Health.Value);
        }

        public void Visit(Sun sun)
        {
            TakeDamage(_playerModel.Health.Value);
        }
    }
}