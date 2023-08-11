using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Entities
{
    public class EntityProvider : MonoBehaviour
    {
        private Entity _entity;
        public ref Entity Entity => ref _entity;
        public World World { get; private set; }

        public void Init(World world)
        {
            _entity = world.CreateEntity();
            World = world;
            OnInit();
        }

        public void Init(World world, Entity entity)
        {
            _entity = entity;
            World = world;
            OnInit();
        }

        protected virtual void OnInit()
        {
        }

        public void Dispose()
        {
            if (_entity != null)
                World.RemoveEntity(_entity);

            World = null;
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        public void SetData<T>(T component) where T : struct, IComponent
        {
            if (_entity.ID == EntityId.Invalid)
                throw new NullReferenceException("Trying to get a component of a dead entity");

            var pool = World.GetStash<T>();

            if (pool.Has(_entity) == false)
            {
                pool.Add(_entity);
            }

            pool.Get(_entity) = component;
        }

        public ref T GetData<T>() where T : struct, IComponent
        {
            if (_entity != null)
            {
                return ref World.GetStash<T>().Get(_entity);
            }

            throw new NullReferenceException("Trying to get a component of a dead entity");
        }
    }
}