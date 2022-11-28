using Leopotam.Ecs;
using Model.Components.Tags;
using Model.Timers;

namespace Model.Systems.Weapons.Particle
{
    public sealed class CheckSunGravityResistSystem : IEcsRunSystem
    {
        private readonly EcsFilter<NoGravity>.Exclude<Timer<SunGravityResistTime>> _filter = null; 
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Del<NoGravity>();
            }
        }
    }
}