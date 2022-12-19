using Extensions;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Tags;
using UnityEngine;

namespace Controller.EntityToGameObject
{
    public class PlayerUnityNotify : EcsUnityNotifierBase
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (Entity.IsAlive() == false)
                return;

            var provider = col.transform.GetProvider();
            if (!provider) return;
            
            if (provider.Entity.IsAlive() && provider.Entity.Has<PlayerTag>())
            {
                Entity.AddEvent(new CollisionEnterEvent() {Other = provider.Entity});
            }
        }
    }
}