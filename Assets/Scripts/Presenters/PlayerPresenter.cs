using Models.Player;
using Models.Player.Interfaces;
using Models.Player.Weapon.Bullets;
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
            if (col.transform.TryGetComponent<IDamageVisitor>(out var damageVisitor))
            {
                var health = damageVisitor.Health;
                damageVisitor.Visit(this);
                TakeDamage(health);
            }
        }

        private void TakeDamage(float value)
        {
            _damageHandler.TakeDamage(value);
        }
        
        public Vector3 Position => _playerModel.Position;
        public float Health => _playerModel.Health.Value;
        public PlayerModel Model => _playerModel;

        public void Visit(DamagerModel damager)
        {
            TakeDamage(damager.Damage);
        }

        public void Visit(IDamageVisitor playerModel)
        {
            TakeDamage(playerModel.Health);
        }

        public void Visit(Sun sun)
        {
            TakeDamage(_playerModel.Health.Value);
        }
    }
}