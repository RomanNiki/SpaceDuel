using Components.Events;
using Components.Unit;
using Leopotam.Ecs;

namespace Systems
{
    public class DeathSystem : IEcsRunSystem
    {
        private EcsFilter<DyingPolicy, Health, HealthChangeEvent> _deathFilter;

        public void Run()
        {
            foreach (var i in _deathFilter)
            {
                ref var dyingPolicy = ref _deathFilter.Get1(i);
                ref var health = ref _deathFilter.Get2(i).Current;
                CheckDeath(dyingPolicy, health.Value, i);
            }
        }

        private void CheckDeath(in DyingPolicy dyingPolicy, in float health, int ind)
        {
            if (dyingPolicy.Policy.Died(health) == false) return;
            ref var entity = ref _deathFilter.GetEntity(ind);
            entity.Get<PlayerDiedEvent>();
        }
    }
}