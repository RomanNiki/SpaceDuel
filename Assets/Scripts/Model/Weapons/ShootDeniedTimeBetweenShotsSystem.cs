using Leopotam.Ecs;
using Model.Timers.Components;
using Model.Weapons.Components;

namespace Model.Weapons
{
    public sealed class ShootDeniedTimeBetweenShotsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Shooting, Timer<TimerBetweenShots>> _filter = null; 
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Del<Shooting>();
            }
        }
    }
}