using Model.Components.Extensions.Pool;
using Model.Pause;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Views
{
    public class VisualEffectView :  MonoBehaviour, IPoolable<IMemoryPool>, IVisualEffectPoolObject, IPauseHandler
    {
        [SerializeField] private VisualEffect _visualEffect;
        public VisualEffect VisualEffect => _visualEffect;
        private IMemoryPool _memoryPool;
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }

        public void OnDespawned()
        {
            _memoryPool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _memoryPool = pool;
        }

        public void PoolRecycle()
        {
            _memoryPool.Despawn(this);
        }
        
        public void SetPaused(bool isPaused)
        {
            _visualEffect.pause = isPaused;
        }

        public class Factory : PlaceholderFactory<VisualEffectView>
        {
        }
    }
}