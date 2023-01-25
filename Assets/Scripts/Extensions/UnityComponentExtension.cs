using Controller.EntityToGameObject;
using Leopotam.Ecs;
using Model.Extensions;
using UnityEngine;

namespace Extensions
{
    public static class UnityComponentExtension
    {
        public static EcsUnityProvider GetProvider(this Component component)
        {
            var gameObject = component.gameObject;

            var providerExist = gameObject.TryGetComponent(out EcsUnityProvider provider);
            if (!providerExist)
            {
                provider = gameObject.AddComponent<EcsUnityProvider>();
            }

            return provider;
        }

        public static bool HasProvider(this Component component, out EcsUnityProvider provider)
        {
            var gameObject = component.gameObject;
            if (gameObject.TryGetComponent(out provider) == false)
            {
                provider = gameObject.GetComponentInParent<EcsUnityProvider>();
            }

            return provider != null;
        }

        public static void AddEventToStack<T>(in this EcsEntity entity)
            where T : struct
        {
            var eventComponent = new T();
            AddEventToStack(entity, eventComponent);
        }

        public static void AddEventToStack<T>(in this EcsEntity entity, in T eventComponent)
            where T : struct
        {
            ref var containerComponents = ref entity.Get<ContainerComponents<T>>();
            containerComponents.List.Add(eventComponent);
        }

        public static void AddEvent<T>(in this EcsEntity entity, in T eventComponent)
            where T : struct
        {
            entity.Get<T>() = eventComponent;
        }
    }
}