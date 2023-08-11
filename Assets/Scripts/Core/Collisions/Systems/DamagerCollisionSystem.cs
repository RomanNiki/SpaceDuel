using Core.Collisions.Components;
using Core.Damage.Components;
using Core.Player.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Systems
{
    public class DamagerCollisionSystem : CollisionSystemBase<DamageContainer>
    {
        private Stash<Health> _healthPool;
        private Stash<DamageContainer> _damageContainerPool;
        private Stash<DamageRequest> _damageRequestPool;

        protected override void OnInit()
        {
            _healthPool = World.GetStash<Health>();
            _damageContainerPool = World.GetStash<DamageContainer>();
            _damageRequestPool = World.GetStash<DamageRequest>();
        }

        protected override bool TryCollide(CollisionEvent collisionEvent, in Entity entity)
        {
            return collisionEvent.Other != null && TryAddDamage(collisionEvent.Other, entity);
        }
        
        private bool TryAddDamage(Entity otherEntity, Entity entity)
        {
            if (_healthPool.Has(otherEntity) == false) return false;

            if (_damageRequestPool.Has(otherEntity) == false)
            {
                _damageRequestPool.Add(otherEntity);
            }

            _damageRequestPool.Get(otherEntity).Value +=
                _damageContainerPool.Get(entity).DamageRequest.Value;
            return true;
        }
    }
}