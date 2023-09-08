using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers
{
    [RequireComponent(typeof(EntityProvider))]
    public abstract class EcsUnityNotifierBase : MonoBehaviour
    {
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

        private EntityProvider _provider;
    }
}