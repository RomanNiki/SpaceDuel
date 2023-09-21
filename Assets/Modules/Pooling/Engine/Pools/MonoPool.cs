using Modules.Pooling.Core.Factory;
using Modules.Pooling.Core.Pool;
using UnityEngine;

namespace Modules.Pooling.Engine.Pools
{
    public class MonoPool<TComponent> : Pool<TComponent>
        where TComponent : Component, IPoolItem
    {
        private Transform _originalParent;

        public MonoPool(Settings settings, IFactory<TComponent> factory) : base(settings, factory)
        {
        }

        public MonoPool(IFactory<TComponent> factory) : base(factory)
        {
        }

        protected override void OnCreated(TComponent item)
        {
            item.gameObject.SetActive(false);
            _originalParent = item.transform.parent;
        }

        protected override void OnDestroyed(TComponent item)
        {
            Object.Destroy(item.gameObject);
        }

        protected override void OnDespawned(TComponent item)
        {
            if (item == null) return;
            item.OnDespawned();
            item.gameObject.SetActive(false);

            if (item.transform.parent != _originalParent)
            {
                item.transform.SetParent(_originalParent);
            }
        }

        protected override void ReInitialize(TComponent item)
        {
            item.OnSpawned(this);
            item.gameObject.SetActive(true);
        }

        protected override void OnActiveItemDispose(TComponent item)
        {
            if (item)
            {
                item.Dispose();
            }
        }

        protected override void OnInactiveItemDispose(TComponent item)
        {
            if (item)
            {
                Object.Destroy(item.gameObject);
            }
        }
    }
}