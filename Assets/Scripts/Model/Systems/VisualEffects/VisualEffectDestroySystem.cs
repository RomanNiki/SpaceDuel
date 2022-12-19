using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Tags.Effects;
using Model.Timers;

namespace Model.Systems.VisualEffects
{
    public sealed class VisualEffectDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<VisualEffectTag>.Exclude<Timer<LifeTime>> _filterExplosion = null;
        
        public void Run()
        {
            foreach (var i in _filterExplosion)
            {
                _filterExplosion.GetEntity(i).Get<EntityDestroyRequest>();
            }
        }
    }
}