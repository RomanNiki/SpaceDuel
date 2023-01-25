using Leopotam.Ecs;
using Model.Timers.Components;
using Model.Unit.Damage.Components;
using Model.Unit.Damage.Components.Requests;
using Model.Unit.Destroy.Components.Requests;

namespace Model.Timers
{
    public sealed class LifeTimeTimerSystem<TTag> : IEcsRunSystem
    where TTag : struct
    {
        private readonly EcsFilter<TTag>.Exclude<Timer<LifeTime>> _filterExplosion = null;
        
        public void Run()
        {
            foreach (var i in _filterExplosion)
            {
                ref var entity = ref _filterExplosion.GetEntity(i);
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