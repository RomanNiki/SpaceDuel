using Components.Events;
using Components.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Extensions.EntityToGameObject
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
                Entity.AddEventToStack(new CollisionEvent() {Other = provider.Entity});
            }
        }
    }
}