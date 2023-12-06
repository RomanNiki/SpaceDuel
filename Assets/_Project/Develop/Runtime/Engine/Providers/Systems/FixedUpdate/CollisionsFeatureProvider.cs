using _Project.Develop.Runtime.Core.Collisions;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.FixedUpdate
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/FixedUpdate/" + nameof(CollisionsFeature))]
    public sealed class CollisionsFeatureProvider : FixedUpdateFeatureProvider<CollisionsFeature>
    {}
}