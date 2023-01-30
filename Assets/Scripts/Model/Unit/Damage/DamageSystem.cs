using Leopotam.Ecs;
using Model.Unit.Damage.Components;
using Model.Unit.Damage.Components.Events;
using Model.Unit.Damage.Components.Requests;
using UnityEngine;

namespace Model.Unit.Damage
{
    public sealed class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageRequest, Health>.Exclude<InstantlyKillRequest> _damageRequestFilter;
        private EcsFilter<Health, InstantlyKillRequest> _instantlyKillFilter;

        public void Run()
        {
            foreach (var i in _damageRequestFilter)
            {
                ref var damage = ref _damageRequestFilter.Get1(i);
                ref var health = ref _damageRequestFilter.Get2(i);
                ref var entity = ref _damageRequestFilter.GetEntity(i);
                TakeDamage(ref health, damage.Damage, ref entity);
              
            }

            foreach (var i in _instantlyKillFilter)
            {
                ref var entity = ref _instantlyKillFilter.GetEntity(i);
                ref var health = ref _instantlyKillFilter.Get1(i);
                TakeDamage(ref health, health.Current, ref entity);
            }
        }
        
        private static void TakeDamage(ref Health health, float damage, ref EcsEntity entity)
        {
            health.Current = Mathf.Max(0.0f, health.Current - damage);
            entity.Get<HealthChangeEvent>();
        }
    }
}