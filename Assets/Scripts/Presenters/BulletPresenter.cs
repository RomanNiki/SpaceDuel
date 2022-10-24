using Models.Player.Interfaces;
using Models.Player.Weapon.Bullets;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class BulletPresenter : MonoBehaviour, IPoolable<float, Vector3, Vector3, IMemoryPool>
    {
        [Inject] private DamagerModel _bulletModel;
        private IMemoryPool _pool;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.usedByEffector)
                return;
            
            if (col.transform.TryGetComponent(out IDamageVisitor visitor))
            {
                visitor.Visit(_bulletModel);
                _pool?.Despawn(this);
            }
            
            _pool?.Despawn(this);
        }

        public void OnDespawned()
        {
            _bulletModel.EnergyEnded -= OnEnergyEnded;
            _bulletModel.Reset();
            _pool = null;
        }

        public void OnSpawned(float force, Vector3 position, Vector3 dir, IMemoryPool pool)
        {
            _bulletModel.EnergyEnded += OnEnergyEnded;
            _bulletModel.Position = position;
            _pool = pool;
            _bulletModel.AddForce(dir * force);
        }

        private void OnEnergyEnded()
        {
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<float, Vector3, Vector3, BulletPresenter>
        {
        }
    }
}