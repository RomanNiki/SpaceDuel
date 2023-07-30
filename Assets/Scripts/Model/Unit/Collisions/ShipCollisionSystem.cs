using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Collisions.Components.Events;
using Model.Unit.Damage.Components;
using Model.Unit.Damage.Components.Requests;
using Model.Unit.Movement.Components.Tags;

namespace Model.Unit.Collisions
{
    public sealed class ShipCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, PlayerTag> _playerFilter =
            null;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                var collision = _playerFilter.Get1(i).List;
                var entity = _playerFilter.GetEntity(i);
                for (var j = 0; j < collision.Count; j++)
                {
                    var triggerEvent = collision.Dequeue();
                    var otherEntity = triggerEvent.Other;
                    if (otherEntity.IsAlive() == false) continue;

                    if (otherEntity.Has<Health>() == false && otherEntity.Has<PlayerTag>() == false)
                    {
                        collision.Enqueue(triggerEvent);
                        continue;
                    }

                    ref var otherHealth = ref otherEntity.Get<Health>();
                    entity.Get<DamageRequest>().Damage = otherHealth.Current;
                }
            }
        }
    }
}