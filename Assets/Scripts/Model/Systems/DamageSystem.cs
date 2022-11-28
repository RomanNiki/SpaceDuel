using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Components.Unit;
using UnityEngine;

namespace Model.Systems
{
    public sealed class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageRequest, Health> _damageRequestFilter;

        public void Run()
        {
            foreach (var i in _damageRequestFilter)
            {
                ref var damage = ref _damageRequestFilter.Get1(i);
                ref var health = ref _damageRequestFilter.Get2(i);
                
                TakeDamage(ref health, damage.Damage);
                ref var entity = ref _damageRequestFilter.GetEntity(i);
                entity.Get<HealthChangeEvent>();
                entity.Get<ViewUpdateRequest>();
            }
        }
        
        private static void TakeDamage(ref Health health, float damage)
        {
            health.Current = Mathf.Max(0.0f, health.Current - damage);
        }
    }
}