﻿using System;
using Leopotam.Ecs;
using Model.Weapons.Components.Tags;
using UnityEngine;

namespace Extensions.MappingUnityToModel.Factories.Weapon.Bullets
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "SpaceDuel/Projectiles/Bullet", order = 10)]
    [Serializable]
    public sealed class BulletEntityFactoryFromSo : ProjectileEntityFactoryFromSo
    {
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = base.CreateEntity(world);
            entity.Get<BulletTag>();
            return entity;
        }
    }
}