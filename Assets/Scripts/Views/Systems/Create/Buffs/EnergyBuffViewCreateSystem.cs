﻿using Extensions;
using Extensions.Systems;
using Leopotam.Ecs;
using Model.Buffs.Components.Tags;
using Model.Components.Requests;
using Model.Extensions;
using UnityEngine;
using Zenject;

namespace Views.Systems.Create.Buffs
{
    public class EnergyBuffViewCreateSystem : ViewCreateSystem<ViewCreateRequest, EnergyBuffTag>
    {
        [Inject] private GameObjectView.Factory _factory;
        protected override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var poolObject = _factory.Create();
            var transform = poolObject.Transform;
            transform.position = data.StartPosition;
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform, poolObject);
            return transform;
        }
    }
}