using _Project.Develop.Runtime.Core.Collisions.Components;
using _Project.Develop.Runtime.Core.Extensions;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Notifiers
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
            if (otherTransform.transform.parent != null)
            {
                var parentProvider = otherTransform.GetComponentInParent<EntityProvider>();
                if (parentProvider == null)
                {
                    return false;
                }
                
                if (parentProvider.Entity.IsNullOrDisposed())
                    return false;
                
                entity = parentProvider.Entity;
                return true;
            }
            
            if (otherTransform.TryGetComponent<EntityProvider>(out var provider) == false)
            {
                return false;
            }
                
            if (provider.Entity.IsNullOrDisposed())
                return false;
            
            entity = provider.Entity;
            return true;
        }
    }
}