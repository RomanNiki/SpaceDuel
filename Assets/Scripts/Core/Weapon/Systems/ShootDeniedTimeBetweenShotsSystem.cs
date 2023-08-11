using Core.Timers.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;

namespace Core.Weapon.Systems
{
    public class ShootDeniedTimeBetweenShotsSystem : ISystem
    {
        private Filter _filter;
        private Stash<Shooting> _shootingPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<Shooting>().With<Timer<TimerBetweenShots>>();
            _shootingPool = World.GetStash<Shooting>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _shootingPool.Remove(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}