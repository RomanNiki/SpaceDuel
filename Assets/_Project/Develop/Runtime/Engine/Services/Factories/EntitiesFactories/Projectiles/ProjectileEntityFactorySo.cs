using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories.EntitiesFactories.Projectiles
{
    public abstract class ProjectileEntityFactorySo : EntityFactoryFromSo
    {
        [SerializeField] private float _health = 1;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private Friction _friction = new() { Value = 0.001f };
        [SerializeField] private Mass _mass = new() { Value = 0.1f };

        public override Entity CreateEntity(in World world)
        {
            var entity = world.CreateEntity();
            OnCreateProjectileEntity(entity, world);
            world.AddComponentToEntity(entity, new ProjectileTag());
            world.AddComponentToEntity(entity, new Health(_health));
            world.AddComponentToEntity(entity, new Position());
            world.AddComponentToEntity(entity, new Rotation());
            world.AddComponentToEntity(entity, _friction);
            world.AddComponentToEntity(entity, _mass);
            world.AddComponentToEntity(entity, new Velocity());
            world.AddComponentToEntity(entity, new Damage { Value = _damage });
            return entity;
        }

        protected abstract void OnCreateProjectileEntity(Entity entity, in World world);
    }
}