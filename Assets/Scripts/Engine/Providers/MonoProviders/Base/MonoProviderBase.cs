using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.Base
{
    public abstract class MonoProviderBase : MonoBehaviour, IProvider
    {
        public abstract void Resolve(World world, Entity entity);
    }
}