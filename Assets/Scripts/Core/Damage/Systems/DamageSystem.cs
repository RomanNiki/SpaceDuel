using Core.Damage.Components;
using Core.Player.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Damage.Systems
{
    public sealed class DamageSystem : ISystem
    {
        private Filter _damageFilter;
        private Stash<Health> _healthPool;
        private Stash<KillRequest> _killRequestPool;
        private Stash<DamageRequest> _damageRequestPool;
        private Stash<HealthChangedEvent> _damagedEvent;

        public World World { get; set; }

        public void OnAwake()
        {
            _damageFilter = World.Filter.With<DamageRequest>().With<Health>().Without<KillRequest>().Without<DeadTag>();
            _healthPool = World.GetStash<Health>();
            _killRequestPool = World.GetStash<KillRequest>();
            _damagedEvent = World.GetStash<HealthChangedEvent>();
            _damageRequestPool = World.GetStash<DamageRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _damageFilter)
            {
                ref var health = ref _healthPool.Get(entity);
                ref var damageRequest = ref _damageRequestPool.Get(entity).Value;
                health.Value = Mathf.Clamp(health.Value - damageRequest, 0f, health.MaxValue);
                _damagedEvent.Add(entity);
                _damageRequestPool.Remove(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}