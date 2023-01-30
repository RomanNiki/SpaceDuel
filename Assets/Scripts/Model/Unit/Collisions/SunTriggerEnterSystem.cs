using Leopotam.Ecs;
using Model.Components;
using Model.Extensions;
using Model.Unit.Collisions.Components.Events;
using Model.Unit.Damage.Components.Requests;

namespace Model.Unit.Collisions
{
    public sealed class SunTriggerEnterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, Sun> _sunCollisionFilter =
            null;

        public void Run()
        {
            foreach (var i in _sunCollisionFilter)
            {
                var collisions = _sunCollisionFilter.Get1(i).List;

                foreach (var collision in collisions)
                {
                    collision.Other.Get<InstantlyKillRequest>();
                }
            }
        }
    }
}