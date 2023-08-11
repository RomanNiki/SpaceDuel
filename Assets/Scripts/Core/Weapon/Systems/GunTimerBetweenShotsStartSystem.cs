using Core.Timers.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;

namespace Core.Weapon.Systems
{
    public class GunTimerBetweenShotsStartSystem : ISystem
    {
        private Filter _filter;
        private Stash<Timer<TimerBetweenShots>> _timerPool;
        private Stash<TimerBetweenShotsSetup> _timerSetupPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<TimerBetweenShotsSetup>().With<ShotMadeEvent>();
            _timerPool = World.GetStash<Timer<TimerBetweenShots>>();
            _timerSetupPool = World.GetStash<TimerBetweenShotsSetup>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _timerPool.Add(entity) = new Timer<TimerBetweenShots>
                    { TimeLeftSec = _timerSetupPool.Get(entity).TimeSec };
            }
        }
        
        public void Dispose()
        {
        }
    }
}