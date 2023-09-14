using Core.Views.Components;
using Engine.Movement.Strategies;
using Engine.Providers.MonoProviders.Base;
using Engine.Views;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.View
{
    public class RigidbodyViewObjectMonoProvider : MonoProviderBase
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private EntityProvider _entityProvider;

        public override void Resolve(World world, Entity entity)
        {
            var moveStrategy = new RigidBodyMoveStrategy(_rigidbody);
            var viewObject = new UnityViewObject(_entityProvider, moveStrategy);
            world.GetStash<ViewObject>().Set(entity, new ViewObject { Value = viewObject });
        }
    }
}