﻿using Leopotam.Ecs;
using Model.Components.Extensions;
using Model.Components.Extensions.DyingPolicies;
using Model.Components.Requests;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Extensions.Factories
{
    public abstract class ProjectileEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private float _health = 1;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _friction = 0.1f;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();

            entity.AddHealth(_health, new StandardDyingPolicy())
                .AddMove(Vector2.zero, 0f, 0.1f, _friction);
            entity.Get<DamageContainer>().DamageRequest = new DamageRequest {Damage = _damage};
            return entity;
        }
    }
}