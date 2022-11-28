using Leopotam.Ecs;
using Model.Components.Extensions.DyingPolicies;
using Model.Components.Requests;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Extensions.Factories
{
    public abstract class ProjectileEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private Health _health = new () { Current = 1};
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _friction = 0.1f;
        
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
         
            entity.Get<Health>() = _health;
            entity.Get<DyingPolicy>().Policy = new StandardDyingPolicy();
            entity.Get<TransformData>();
            entity.Get<Move>();
            entity.Get<Friction>().Value = _friction;
            entity.Get<DamageContainer>().DamageRequest = new DamageRequest {Damage = _damage};
            return entity;
        }
    }
}