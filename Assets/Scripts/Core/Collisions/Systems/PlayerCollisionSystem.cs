using Core.Collisions.Components;
using Core.Damage.Components;
using Core.Player.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Systems
{
    public sealed class PlayerCollisionSystem : CollisionSystemBase<PlayerTag>
    {
        private Stash<PlayerTag> _playerTagPool;
        private Stash<Health> _healthPool;
        private Stash<DamageRequest> _damageRequestPool;

        protected override void OnInit()
        {
            _playerTagPool = World.GetStash<PlayerTag>();
            _healthPool = World.GetStash<Health>();
            _damageRequestPool = World.GetStash<DamageRequest>();
        }
        
        protected override bool TryCollide(CollisionEvent collisionEvent, in Entity entity)
        {
            if (collisionEvent.Other == null)
            {
                return false;
            }

            if (_playerTagPool.Has(collisionEvent.Other) == false) return false;
            
            if (_damageRequestPool.Has(collisionEvent.Other) == false)
            {
                _damageRequestPool.Add(collisionEvent.Other);
            }

            _damageRequestPool.Get(collisionEvent.Other).Value += _healthPool.Get(entity).Value;

            return true;
        }
    }
}