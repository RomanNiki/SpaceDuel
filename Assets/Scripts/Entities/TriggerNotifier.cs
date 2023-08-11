using Core.Collisions.Components;
using Extensions;
using Scellecs.Morpeh;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Collider))]
    public class TriggerNotifier : EcsUnityNotifierBase
    {
        private void OnTriggerEnter(Collider other)
        {
            if (Entity == null)
                return;

            if (TryGetEntity(other.transform, out var otherEntity))
                Entity.AddEventToStack(World, new CollisionEvent() { Other = otherEntity });
        }

        private static bool TryGetEntity(Component otherTransform, out Entity entity)
        {
            entity = null;
            if (otherTransform.TryGetComponent<EntityProvider>(out var provider) == false)
                return false;
            if (provider.Entity == null)
                return false;

            entity = provider.Entity;
            return true;
        }
    }
}