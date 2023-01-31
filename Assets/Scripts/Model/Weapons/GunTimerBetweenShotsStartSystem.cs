using Leopotam.Ecs;
using Model.Timers.Components;
using Model.Weapons.Components.Events;

namespace Model.Weapons
{
    public sealed class GunTimerBetweenShotsStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TimeBetweenShotsSetup, ShotMadeEvent> _shotMadeFilter = null;
        
        public void Run()
        {
            foreach (var i in _shotMadeFilter)
            {
                ref var timeBetweenShotsComponent = ref _shotMadeFilter.Get1(i);
                ref var gun = ref _shotMadeFilter.GetEntity(i);
                gun.Get<Timer<TimerBetweenShots>>().TimeLeftSec = timeBetweenShotsComponent.TimeSec;
            }
        }
    }
}