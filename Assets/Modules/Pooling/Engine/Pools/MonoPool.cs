using System;
using Modules.Pooling.Core.Factory;
using Modules.Pooling.Core.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Modules.Pooling.Engine.Pools
{
    public class MonoPool<TComponent> : Pool<TComponent>
        where TComponent : Component, IPoolItem
    {
        private readonly Vector3 _outGamePoint = new(-25, -25, -25);
        private readonly Lazy<Scene> _rootScene;

        public MonoPool(Settings settings, IFactory<TComponent> factory, string name) : base(settings, factory)
        {
            _rootScene = new Lazy<Scene>(() => SceneManager.CreateScene($"[Pool] {name}"));
        }

        protected override void OnCreated(TComponent item)
        {
            MoveToPool(item);
        }

        protected override void OnDespawned(TComponent item)
        {
            if (item == null) return;
            item.OnDespawned();
            MoveToPool(item);
        }

        protected override void OnDestroyed(TComponent item)
        {
            Object.Destroy(item.gameObject);
        }

        protected override void ReInitialize(TComponent item)
        {
            item.OnSpawned(this);
            item.gameObject.SetActive(true);
        }

        protected override void OnActiveItemDispose(TComponent item)
        {
            item.Dispose();
        }

        protected override void OnInactiveItemDispose(TComponent item)
        {
            if (item)
            {
                Object.Destroy(item.gameObject);
            }
        }
        
        private void MoveToPool(TComponent item)
        {
            item.transform.position = _outGamePoint;
            var gameObject = item.gameObject;
            gameObject.SetActive(false);
            SceneManager.MoveGameObjectToScene(gameObject, _rootScene.Value);
        }

        protected override void OnDispose()
        {
            SceneManager.UnloadSceneAsync(_rootScene.Value);
        }
    }
}