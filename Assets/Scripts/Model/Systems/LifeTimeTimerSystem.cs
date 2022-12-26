using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Unit;
using Model.Timers;

namespace Model.Systems
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