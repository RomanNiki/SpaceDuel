using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using Model.Components.Extensions;
using Model.Components.Requests;


namespace Model.Systems.Unit.Collisions
{
    public sealed class SunTriggerEnterSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, Sun> _filter =
            null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var collisions = _filter.Get1(i).List;

                foreach (var collision in collisions)
                {
                    collision.Other.Get<InstantlyKillRequest>();
                }
            }
        }
    }
}