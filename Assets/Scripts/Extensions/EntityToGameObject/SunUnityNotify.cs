using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Extensions.EntityToGameObject
{
    public sealed class SunUnityNotify : EcsUnityNotifierBase
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var otherTransform = col.transform;
            if (otherTransform.HasProvider() == false) 
                return;

            var otherEntity = otherTransform.GetProvider().Entity;
            if (otherEntity.IsAlive() == false) 
                return;
            
            Entity.AddEventToStack(new CollisionEvent() {Other = otherEntity});
        }
    }
} 