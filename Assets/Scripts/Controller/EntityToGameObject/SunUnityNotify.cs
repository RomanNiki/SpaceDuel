using Extensions;
using Leopotam.Ecs;
using Model.Components.Events;
using UnityEngine;

namespace Controller.EntityToGameObject
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
            
            Entity.AddEventToStack(new TriggerEnterEvent() {Other = otherEntity});
        }
    }
} 