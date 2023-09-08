using System;
using Engine.Converters.Base;
using Engine.ScriptableObjects.Settings;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class EntityProvider : Scellecs.Morpeh.Providers.EntityProvider, IDisposable
    {
        [SerializeField] private EntityConfiguration[] _entityConfigurations;
        [SerializeReference] private IConverter[] _converters;
        private bool _disposed = true;
        public World World { get; private set; }

        public void Init(World world)
        {
            cachedEntity = world.CreateEntity();
            World = world;
            Init();
        }

        public void Init(World world, Entity entity)
        {
            cachedEntity = entity;
            World = world;
            Init();
        }

        private void Init()
        {
            _disposed = false;

            foreach (var link in _converters)
            {
                link.Resolve(World, Entity);
            }

            foreach (var entityConfiguration in _entityConfigurations)
            {
                foreach (var link in entityConfiguration.Links)
                {
                    link.Resolve(World, Entity);
                }
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
            if (cachedEntity.IsNullOrDisposed() == false)
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
    }
}