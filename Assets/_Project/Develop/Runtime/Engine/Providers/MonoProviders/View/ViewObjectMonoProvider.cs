﻿using _Project.Develop.Runtime.Core.Views.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using _Project.Develop.Runtime.Engine.Services.Movement.Strategies;
using _Project.Develop.Runtime.Engine.Views;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.View
{
    [RequireComponent(typeof(EntityProvider))]
    public class ViewObjectMonoProvider : MonoProviderBase
    {
        private EntityProvider _entityProvider;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _entityProvider = GetComponent<EntityProvider>();
        }

        public override void Resolve(World world, Entity entity)
        {
            IMoveStrategy moveStrategy;
            if (_rigidbody != null)
            {
                moveStrategy = new RigidBodyMoveStrategy(_rigidbody);
            }
            else
            {
                moveStrategy = new TranslateMoveStrategy(transform);
            }
            var viewObject = new UnityViewObject(_entityProvider, moveStrategy);
            world.GetStash<ViewObject>().Set(entity, new ViewObject { Value = viewObject });
        }
    }
}