using Leopotam.Ecs;
using Model.Extensions.DyingPolicies;
using Model.Unit.Damage.Components;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Model.Extensions
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
        
        private static ref EcsEntity AddPhysicsMove(this ref EcsEntity entity, float mass, float friction)
        {
            entity.Get<Velocity>();
            entity.Get<Mass>().Value = mass;
            entity.Get<Friction>().Value = friction;
            return ref entity;
        }

        public static ref EcsEntity AddTransform(this ref EcsEntity entity, Vector2 startPosition, float startRotation = 0f)
        {
            entity.Get<Position>().Value = startPosition;
            entity.Get<Rotation>().Value = startRotation;
            return ref entity;
        } 
        
        public static ref EcsEntity AddMovementComponents(this ref EcsEntity entity, Vector2 startPosition, float startRotation, float mass, float friction = 0.5f)
        {
            entity.AddTransform(startPosition, startRotation);
            entity.AddPhysicsMove(mass, friction);
            return ref entity;
        }
    }
}