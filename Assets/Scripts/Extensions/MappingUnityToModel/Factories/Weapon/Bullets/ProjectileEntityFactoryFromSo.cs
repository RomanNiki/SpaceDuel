using Leopotam.Ecs;
using Model.Extensions;
using Model.Extensions.DyingPolicies;
using Model.Unit.Damage.Components;
using Model.Unit.Damage.Components.Requests;
using Model.Weapons.Components.Tags;
using UnityEngine;

namespace Extensions.MappingUnityToModel.Factories.Weapon.Bullets
{
    public abstract class ProjectileEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private float _health = 1;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _friction = 0.1f;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<ProjectileTag>();
            entity.AddHealth(_health, new StandardDyingPolicy())
                .AddMovementComponents(Vector2.zero, 0f, 0.1f, _friction);
            entity.Get<DamageContainer>().DamageRequest = new DamageRequest {Damage = _damage};
            return entity;
        }
    }
}