using Leopotam.Ecs;
using Model.Timers.Components;
using Model.Unit.Movement.Components.Tags;

namespace Model.Unit.Movement
{
    public sealed class CheckSunGravityResistSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GravityResist>.Exclude<Timer<SunGravityResistTime>> _filter = null; 
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Del<GravityResist>();
            }
        }
    }
}