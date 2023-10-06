using Core.Views.Components;
using Engine.Providers.MonoProviders.Base;
using Engine.Services.Movement.Strategies;
using Engine.Views;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.View
{
    public class TransformViewObjectMonoProvider : MonoProviderBase
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private EntityProvider _entityProvider;

        public override void Resolve(World world, Entity entity)
        {
            var moveStrategy = new TranslateMoveStrategy(_transform);
            var viewObject = new UnityViewObject(_entityProvider, moveStrategy);
            world.GetStash<ViewObject>().Set(entity, new ViewObject { Value = viewObject });
        }
    }
}