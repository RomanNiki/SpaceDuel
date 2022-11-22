using Components.Events;
using Components.Requests;
using Components.Unit;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageRequest, Health> _damageRequestFilter;

        public void Run()
        {
            foreach (var i in _damageRequestFilter)
            {
                ref var damage = ref _damageRequestFilter.Get1(i);
                ref var health = ref _damageRequestFilter.Get2(i);
                
                TakeDamage(ref health, damage.Damage);
                if (health.Current.Value == 0) continue;
                ref var entity = ref _damageRequestFilter.GetEntity(i);
                entity.Get<HealthChangeEvent>();
                entity.Get<ViewUpdateRequest>();
            }
        }
        
        private static void TakeDamage(ref Health health, float damage)
        {
            health.Current.Value = Mathf.Max(0.0f, health.Current.Value - damage);
        }
    }
}