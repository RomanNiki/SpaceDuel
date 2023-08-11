using Core.Damage.Components;
using Core.Player.Components;
using Scellecs.Morpeh;

namespace Core.Damage.Systems
{
    public sealed class CheckDeathSystem : ISystem
    {
        private Filter _healthFilter;
        private Stash<Health> _healthPool;
        private Stash<DyingPolicy> _dyingPolicyPool;
        private Stash<DeadTag> _deadTagPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _healthFilter = World.Filter.With<Health>().Without<DeadTag>();
            _healthPool = World.GetStash<Health>();
            _dyingPolicyPool = World.GetStash<DyingPolicy>();
            _deadTagPool = World.GetStash<DeadTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _healthFilter)
            {
                ref var health = ref _healthPool.Get(entity);
                if (_dyingPolicyPool.Has(entity))
                {
                    if (_dyingPolicyPool.Get(entity).Policy.CheckDeath(health.Value))
                    {
                        _deadTagPool.Add(entity);
                    }
                    continue;
                }
                if (health.Value <= 0f)
                {
                    
                    _deadTagPool.Add(entity);
                }
            }
        }
        
        public void Dispose()
        {
        }
    }
}