using _Project.Develop.Runtime.Core.Views.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using _Project.Develop.Runtime.Engine.Providers.Views;
using _Project.Develop.Runtime.Engine.Services.Movement.Strategies;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.View
{
    [RequireComponent(typeof(EntityProvider))]
    public class ViewObjectMonoProvider : MonoProviderBase
    {
        [SerializeField] private ParticleSystem _particleSystem;
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

            UnityViewObject viewObject;

            if (_particleSystem != null)
            {
                viewObject = new UnityViewObject(_entityProvider,
                    new MoveWithParticleFacade(moveStrategy, _particleSystem));
            }
            else
            {
                viewObject = new UnityViewObject(_entityProvider, moveStrategy);
            }

            world.GetStash<ViewObject>().Set(entity, new ViewObject { Value = viewObject });
        }
    }
}