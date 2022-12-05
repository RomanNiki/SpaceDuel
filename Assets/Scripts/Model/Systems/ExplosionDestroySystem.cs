using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Timers;

namespace Model.Systems
{
    public class ExplosionDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ExplosionTag>.Exclude<Timer<DestroyTimer>> _filterExplosion = null;
        
        public void Run()
        {
            foreach (var i in _filterExplosion)
            {
                _filterExplosion.GetEntity(i).Get<EntityDestroyRequest>();
            }
        }
    }
}