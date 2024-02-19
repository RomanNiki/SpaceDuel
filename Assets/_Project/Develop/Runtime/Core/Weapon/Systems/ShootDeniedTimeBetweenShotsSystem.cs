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

    public class ShootDeniedTimeBetweenShotsSystem : ISystem
    {
        private Filter _filter;
        private Stash<ShootingRequest> _shootingPool;
        private Stash<Timer<TimerBetweenShots>> _deniedTimerPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<ShootingRequest>().Build();
            _shootingPool = World.GetStash<ShootingRequest>();
            _deniedTimerPool = World.GetStash<Timer<TimerBetweenShots>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                var weaponEntity = _shootingPool.Get(entity).Entity;
                if (weaponEntity.IsNullOrDisposed())
                    continue;
                if (_deniedTimerPool.Has(weaponEntity))
                {
                    _shootingPool.Remove(entity);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}