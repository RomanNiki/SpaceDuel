using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base
{
    public abstract class MonoProviderBase : MonoBehaviour, IProvider
    {
        public abstract void Resolve(World world, Entity entity);
    }
}