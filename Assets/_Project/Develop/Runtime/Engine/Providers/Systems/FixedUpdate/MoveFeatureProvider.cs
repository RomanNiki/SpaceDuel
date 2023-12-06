using _Project.Develop.Runtime.Core.Movement;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.FixedUpdate
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/FixedUpdate/" + nameof(MoveFeature))]
    public sealed class MoveFeatureProvider : BaseFixedUpdateFeatureProvider
    {
        public override FixedUpdateFeature GetFeature(FeaturesArgs args) =>
            new MoveFeature(args.MoveLoopService);
    }
}