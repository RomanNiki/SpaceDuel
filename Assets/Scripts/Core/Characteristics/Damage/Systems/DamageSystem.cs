using Core.Characteristics.Damage.Components;
using Core.Extensions;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class DamageSystem : ISystem
    {
        private Filter _damageFilter;
        private Stash<Health> _healthPool;
        private Stash<DamageRequest> _damageRequestPool;

        public World World { get; set; }

        public void OnAwake()
        {
            _damageFilter = World.Filter.With<DamageRequest>().Build();
            _healthPool = World.GetStash<Health>();
            _damageRequestPool = World.GetStash<DamageRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _damageFilter)
            {
                ref var damageRequest = ref _damageRequestPool.Get(entity);
                var damageRequestEntity = damageRequest.Entity;
                if (damageRequestEntity.IsNullOrDisposed())
                {
                    continue;
                }
                ref var health = ref _healthPool.Get(damageRequestEntity);
                health.Value = Mathf.Clamp(health.Value - damageRequest.Damage, health.MinValue, health.MaxValue);
                World.SendMessage(new HealthChangedEvent { Entity = damageRequestEntity });
                World.RemoveEntity(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}