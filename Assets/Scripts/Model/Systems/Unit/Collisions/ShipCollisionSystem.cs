using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Unit;

namespace Model.Systems.Unit.Collisions
{
    public sealed class ShipCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionEnterEvent, Health, PlayerTag> _playerFilter =
            null;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var collision = ref _playerFilter.Get1(i);
                ref var otherHealth = ref collision.Other.Get<Health>();
                _playerFilter.GetEntity(i).Get<DamageRequest>().Damage = otherHealth.Current;
            }
        }
    }
}