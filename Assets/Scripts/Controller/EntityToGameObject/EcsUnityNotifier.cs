using Extensions;
using Leopotam.Ecs;
using Model.Components.Events;
using UnityEngine;

namespace Controller.EntityToGameObject
{
    public class EcsUnityNotifier : EcsUnityNotifierBase
    {
        private void OnBecameInvisible()
        {
            if(Entity.IsAlive() == false) 
                return;
            Entity.AddEventToStack<OnBecameInvisibleEvent>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (TryGetEntity(col.transform, out var otherEntity))
            {
                Entity.AddEventToStack(new TriggerEnterEvent() {Other = otherEntity});
            }
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (TryGetEntity(col.transform, out var otherEntity))
            {
                Entity.AddEvent(new CollisionEnterEvent() {Other = otherEntity});
            }
        }

        private bool TryGetEntity(Component otherTransform, out EcsEntity entity)
        {
            entity = EcsEntity.Null;
            if(Entity.IsAlive() == false) 
                return false;
            
            if (otherTransform.HasProvider(out var provider) == false) 
                return false;
            var otherEntity = provider.Entity;
            if (otherEntity.IsAlive() == false) 
                return false;
            
            entity = otherEntity;
            return true;
        }
    }
}