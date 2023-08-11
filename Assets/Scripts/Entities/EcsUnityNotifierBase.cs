using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(EntityProvider))]
    public abstract class EcsUnityNotifierBase : MonoBehaviour
    {
        protected ref Entity Entity => ref Provider.Entity;
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