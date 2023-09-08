using Engine.Providers;
using UnityEngine;

namespace Engine.Extensions
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
    }
}