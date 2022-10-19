using Models.Player.Interfaces;
using Models.Player.Weapon.Bullets;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class BulletPresenter : MonoBehaviour, IPoolable<float, float, IMemoryPool>
    {
        [Inject] private BulletModel _bullet;
        private IMemoryPool _pool;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent(out IDamageVisitor visitor))
            {
                visitor.Visit(_bullet);
            }

            _pool.Despawn(this);
        }

        public void OnDespawned()
        { 
            _bullet.EnergyEnded -= OnEnergyEnded;
            _bullet.Reset();
            _pool = null;
        }

        private void OnEnergyEnded()
        {
            _pool.Despawn(this);
        }

        public void OnSpawned(float force, float lifeTime, IMemoryPool pool)
        {
            _pool = pool;
            _bullet.EnergyEnded += OnEnergyEnded;
            _bullet.AddForce(Vector3.forward * force);
            
        }
        
        public class Factory : PlaceholderFactory<float, float, BulletPresenter>
        {
        }
    }
}