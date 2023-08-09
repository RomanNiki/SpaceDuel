using Leopotam.Ecs;
using Model.Timers.Components;
using Model.Unit.Damage.Components;
using Model.Unit.Damage.Components.Requests;
using Model.Unit.Destroy.Components.Requests;

namespace Model.Timers
{
    public sealed class LifeTimeTimerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DieWithoutLifeTimerTag>.Exclude<Timer<LifeTimer>> _explosionFilter = null;

        public void Run()
        {
            foreach (var i in _explosionFilter)
            {
                ref var entity = ref _explosionFilter.GetEntity(i);
                if (entity.Has<Health>())
                {
                    entity.Get<InstantlyKillRequest>();
                    continue;
                }

                entity.Get<EntityDestroyRequest>();
            }
        }
    }
}