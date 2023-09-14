using Core.Collisions.Components;
using Core.Extensions;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers
{
    public class TriggerNotifier : EcsUnityNotifierBase
    {
        private void OnTriggerEnter(Collider other)
        {
            if (Entity == null)
                return;
            
            if (TryGetEntity(other.transform, out var otherEntity))
            {
                World.SendMessage(new TriggerEnterRequest(Entity, otherEntity));
            }
        }

        private static bool TryGetEntity(Component otherTransform, out Entity entity)
        {
            entity = null;
            if (otherTransform.TryGetComponent<EntityProvider>(out var provider) == false)
                return false;
            if (provider.Entity.IsNullOrDisposed())
                return false;
            
            entity = provider.Entity;
            return true;
        }
    }
}