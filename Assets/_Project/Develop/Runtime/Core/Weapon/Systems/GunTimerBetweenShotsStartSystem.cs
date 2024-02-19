using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Weapon.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public class GunTimerBetweenShotsStartSystem : ISystem
    {
        private Filter _filter;
        private Stash<Timer<TimerBetweenShots>> _timerPool;
        private Stash<TimerBetweenShotsSetup> _timerSetupPool;
        private Stash<ShotMadeEvent> _shotMadeEventPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<ShotMadeEvent>().Build();
            _timerPool = World.GetStash<Timer<TimerBetweenShots>>();
            _shotMadeEventPool = World.GetStash<ShotMadeEvent>();
            _timerSetupPool = World.GetStash<TimerBetweenShotsSetup>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                var weaponEntity = _shotMadeEventPool.Get(entity).Weapon;
                if (weaponEntity.IsNullOrDisposed())
                    continue;
                if (_timerPool.Has(weaponEntity))
                    continue;

                _timerPool.Add(weaponEntity) = new Timer<TimerBetweenShots>(_timerSetupPool.Get(weaponEntity).TimeSec);
            }
        }

        public void Dispose()
        {
        }
    }
}