using Leopotam.Ecs;
using Model.Timers.Components;
using Model.Unit.Movement.Components.Tags;

namespace Model.Unit.Movement
{
    public sealed class CheckSunGravityResistSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GravityResist>.Exclude<Timer<SunGravityResistTime>> _gravityResistFilter = null; 
        
        public void Run()
        {
            foreach (var i in _gravityResistFilter)
            {
                _gravityResistFilter.GetEntity(i).Del<GravityResist>();
            }
        }
    }
}