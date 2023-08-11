using Core.Collisions.Components;
using Core.Damage.Components;
using Core.Sun.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Systems
{
    public sealed class GravityPointCollisionSystem : CollisionSystemBase<GravityPoint>
    {
        private Stash<KillRequest> _killRequest;

        protected override void OnInit()
        {
            _killRequest = World.GetStash<KillRequest>();
        }

        protected override bool TryCollide(CollisionEvent collisionEvent, in Entity entity)
        {
            if (collisionEvent.Other.ID == EntityId.Invalid)
                return false;

            if (_killRequest.Has(collisionEvent.Other) == false)
            {
                _killRequest.Add(collisionEvent.Other);
            }

            return true;
        }
    }
}