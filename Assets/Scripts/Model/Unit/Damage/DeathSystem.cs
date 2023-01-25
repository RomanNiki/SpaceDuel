using Leopotam.Ecs;
using Model.Unit.Damage.Components;
using Model.Unit.Damage.Components.Events;
using Model.Unit.Destroy.Components.Requests;

namespace Model.Unit.Damage
{
    public sealed class DeathSystem : IEcsRunSystem
    {
        private EcsFilter<DyingPolicy, Health, HealthChangeEvent> _deathFilter;

        public void Run()
        {
            foreach (var i in _deathFilter)
            {
                ref var dyingPolicy = ref _deathFilter.Get1(i);
                ref var health = ref _deathFilter.Get2(i);
                CheckDeath(dyingPolicy, health.Current, i);
            }
        }

        private void CheckDeath(in DyingPolicy dyingPolicy, in float health, int ind)
        {
            if (dyingPolicy.Policy.Died(health) == false) return;
            ref var entity = ref _deathFilter.GetEntity(ind);
            entity.Get<EntityDestroyRequest>();
        }
    }
}