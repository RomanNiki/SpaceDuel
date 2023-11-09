using Core.Views.Components;
using Engine.Providers.MonoProviders.Base;
using Engine.Services.Movement.Strategies;
using Engine.Views;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.View
{
    [RequireComponent(typeof(EntityProvider))]
    public class ViewObjectMonoProvider : MonoProviderBase
    {
        private Rigidbody _rigidbody;
        private EntityProvider _entityProvider;

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