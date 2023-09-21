using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Factories.EntitiesFactories.UltimateEntityFactories
{
    public class UltimateEntityFactory : EntityFactoryFromSo
    {
        [SerializeReference] private readonly List<IComponent> _components = new();
        
        public override Entity CreateEntity(in World world)
        {
            var entity = world.CreateEntity();
            return SetComponents(world, entity);
        }

        private Entity SetComponents(World world, Entity entity)
        {
            foreach (var component in _components)
            {
                var stash = world.GetReflectionStash(component.GetType());
                stash.Set(entity);
            }

            return entity;
        }
    }
}