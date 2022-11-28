using Leopotam.Ecs;
using Model.Components.Events;
using Model.Timers;

namespace Model.Systems.Weapons
{
    public sealed class GunTimerBetweenShotsStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TimeBetweenShotsSetup, ShotMadeEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var timeBetweenShotsComponent = ref _filter.Get1(i);
                ref var gun = ref _filter.GetEntity(i);
                gun.Get<Timer<TimerBetweenShots>>().TimeLeftSec = timeBetweenShotsComponent.TimeSec;
            }
        }
    }
}