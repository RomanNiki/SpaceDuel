using Models.Player;
using UnityEngine;
using Zenject;

namespace Views
{
    public class PlayerView : MonoBehaviour, IDamageable
    {
        [Inject] private PlayerModel _playerModel;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent<IDamageable>(out var player))
            {
                player.TakeDamage(_playerModel.Health);
            }
        }

        public void TakeDamage(float value)
        {
            _playerModel.TakeDamage(value);
        }
    }
}