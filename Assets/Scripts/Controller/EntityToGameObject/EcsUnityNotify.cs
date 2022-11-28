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
            if(Entity.IsAlive() == false && col.usedByEffector == false) 
                return;
            
            var otherTransform = col.transform;
            if (otherTransform.HasProvider() == false) 
                return;

            var otherEntity = otherTransform.GetProvider().Entity;
            if (otherEntity.IsAlive() == false) 
                return;
            
            Entity.AddEventToStack(new CollisionEnterEvent() {Other = otherEntity});
        }
    }
}