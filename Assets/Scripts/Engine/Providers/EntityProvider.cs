using System;
using Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Editor;
using TriInspector;
using UnityEngine;

namespace Engine.Providers
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class EntityProvider : MonoBehaviour, IDisposable
    {
        private bool _disposed = true;
        public World World { get; private set; }
        public Entity Entity { get; private set; }

        public void Init(World world)
        {
            Entity = world.CreateEntity();
            World = world;
            Init();
        }

        public void Init(World world, Entity entity)
        {
            Entity = entity;
            World = world;
            Init();
        }

        private void Init()
        {
            _entityViewer.getter = () => Entity;
            _disposed = false;
            
            foreach (var provider in GetComponents<IProvider>())
            {
                provider.Resolve(World, Entity);
            }

            OnInit();
        }

        protected virtual void OnInit()
        {
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            if (Entity.IsNullOrDisposed() == false)
                World.RemoveEntity(Entity);

            World = null;
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        public void SetData<T>(T component) where T : struct, IComponent
        {
            if (Entity.IsNullOrDisposed())
                throw new NullReferenceException("Trying to get a component of a dead entity");

            var pool = World.GetStash<T>();

            pool.Set(Entity, component);
        }

        public ref T GetData<T>() where T : struct, IComponent
        {
            if (Entity.IsNullOrDisposed() == false)
            {
                var pool = World.GetStash<T>();
                return ref pool.Get(Entity);
            }

            throw new NullReferenceException("Trying to get a component of a dead entity");
        }
        
#if UNITY_EDITOR
        [PropertyOrder(100)]
        [ShowInInspector]
        [InlineProperty]
        [HideLabel]
        [Title("Debug Info", HorizontalLine = true)]
        private readonly EntityViewer _entityViewer = new();
#endif
    }
}