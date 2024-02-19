using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Notifiers
{
    [RequireComponent(typeof(EntityProvider))]
    public abstract class EcsUnityNotifierBase : MonoBehaviour
    {
        private EntityProvider _provider;
        protected Entity Entity => Provider.Entity;
        protected World World => Provider.World;

        private EntityProvider Provider
        {
            get 
            {
                if (_provider != null) return _provider;
                if (!TryGetComponent(out _provider)) throw new NullReferenceException();
                return _provider;
            }
        }
    }
}