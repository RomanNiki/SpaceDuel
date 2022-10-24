using Models.Player.Interfaces;
using Models.Player.Weapon.Bullets;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Presenters
{
    public class MinePresenter : MonoBehaviour, IPoolable<Vector3, IMemoryPool>
    {
        [SerializeField] private LayerMask _noEnergyMask;
        [SerializeField] private UnityEvent _energyEnded;
        [Inject] private DamagerModel _mineModel;
        private IMemoryPool _pool;
        private LayerMask _defaultMask;

        private void Awake()
        {
            _defaultMask = gameObject.layer;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.usedByEffector)
                return;
            
            if (col.transform.TryGetComponent(out IDamageVisitor visitor))
            {
                visitor.Visit(_mineModel);
                _pool?.Despawn(this);
            }
            
            _pool?.Despawn(this);
        }

        public void OnDespawned()
        {
            gameObject.layer = _defaultMask;
            _mineModel.Reset();
            _mineModel.EnergyEnded -= OnEnergyEnded;
            _pool = null;
        }

        public void OnSpawned(Vector3 position, IMemoryPool pool)
        {
            _pool = pool;
            _mineModel.EnergyEnded += OnEnergyEnded;
            transform.position = position;
        }

        private void OnEnergyEnded()
        {
            _energyEnded.Invoke();
            gameObject.layer = _noEnergyMask;
        }
        
        public class Factory : PlaceholderFactory<Vector3, MinePresenter>
        {
        }
    }
}