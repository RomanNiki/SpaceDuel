using System;
using Leopotam.Ecs;
using Model.Components.Tags;
using Model.Components.Tags.Projectiles;
using UnityEngine;

namespace Extensions.Factories.Weapon.Bullets
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "SpaceDuel/Bullet", order = 10)]
    [Serializable]
    public class BulletEntityFactoryFromSo : ProjectileEntityFactoryFromSo
    {
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = base.CreateEntity(world);
            entity.Get<BulletTag>();
            return entity;
        }
    }
}