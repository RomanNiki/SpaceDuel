using System.Collections.Generic;
using Core.Collisions.Components;
using Entities;
using Scellecs.Morpeh;
using UnityEngine;

namespace Extensions
{
    public static class EntityExtensions
    {
        public static EntityProvider GetProvider(this Component component)
        {
            var gameObject = component.gameObject;

            var providerExist = gameObject.TryGetComponent(out EntityProvider provider);
            if (!providerExist)
            {
                provider = gameObject.AddComponent<EntityProvider>();
            }

            return provider;
        }
        
        public static void AddEventToStack<T>(this Entity entity, World world, T component) 
            where T : struct, IComponent
        {
            var stackPool = world.GetStash<ComponentQueue<T>>();

            if (stackPool.Has(entity) == false)
            {
                stackPool.Add(entity);
                stackPool.Get(entity).Values = new Queue<T>();
            }

            stackPool.Get(entity).Values.Enqueue(component);
        }
    }
}