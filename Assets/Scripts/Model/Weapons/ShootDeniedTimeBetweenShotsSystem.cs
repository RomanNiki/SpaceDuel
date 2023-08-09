using Leopotam.Ecs;
using Model.Timers.Components;
using Model.Weapons.Components;

namespace Model.Weapons
{
    public sealed class ShootDeniedTimeBetweenShotsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Shooting, Timer<TimerBetweenShots>> _weaponTimerFilter = null; 
        
        public void Run()
        {
            foreach (var i in _weaponTimerFilter)
            {
                _weaponTimerFilter.GetEntity(i).Del<Shooting>();
            }
        }
    }
}