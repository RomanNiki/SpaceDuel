using Models.Player.Interfaces;
using Models.Player.Weapon.Bullets;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class BulletPresenter : MonoBehaviour, IPoolable<float, Vector3, Vector3, IMemoryPool>
    {
        [Inject] private BulletModel _bullet;
        private IMemoryPool _pool;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.TryGetComponent(out IDamageVisitor visitor))
            {
                visitor.Visit(_bullet);
                _pool.Despawn(this);
            }
            if (col.transform.TryGetComponent(out Sun _))
            {
                _pool.Despawn(this);
            }
            
        }

        public void OnDespawned()
        { 
            _bullet.EnergyEnded -= OnEnergyEnded;
            _bullet.Reset();
            _pool = null;
        }

        public void OnSpawned(float force, Vector3 position, Vector3 dir, IMemoryPool pool)
        {
            _bullet.Position = position;
            _pool = pool;
            _bullet.AddForce( dir * force);
            _bullet.EnergyEnded += OnEnergyEnded;
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