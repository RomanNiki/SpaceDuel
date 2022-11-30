using Leopotam.Ecs;
using Model.Components.Extensions.DyingPolicies;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Components.Extensions
{
    public static class EntityExtension
    {
        public static ref EcsEntity AddHealth(this ref EcsEntity entity, float count, IDyingPolicy dyingPolicy)
        {
            ref var health = ref entity.Get<Health>();
            health.Initial = count;
            health.Current = health.Initial;
            entity.Get<DyingPolicy>().Policy = dyingPolicy;
            return ref entity;
        } 
        
        public static ref EcsEntity AddEnergy(this ref EcsEntity entity, float count)
        {
            ref var energy = ref entity.Get<Energy>();
            energy.Initial = count;
            energy.Current = energy.Initial;
            return ref entity;
        } 
        
        public static ref EcsEntity AddMove(this ref EcsEntity entity, Vector2 startPosition, float startRotation, float mass, float friction = 0.5f)
        {
            ref var energy = ref entity.Get<TransformData>();
            energy.Position = startPosition;
            energy.Rotation = startRotation;
            entity.Get<Move>();
            entity.Get<Mass>().Value = mass;
            entity.Get<Friction>().Value = friction;
            return ref entity;
        }
    }
}