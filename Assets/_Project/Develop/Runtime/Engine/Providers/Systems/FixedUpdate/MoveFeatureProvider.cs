using _Project.Develop.Runtime.Core.Movement;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.FixedUpdate
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/FixedUpdate/" + nameof(MoveFeature))]
    public sealed class MoveFeatureProvider : FixedUpdateFeatureProvider<MoveFeature>
    {
    }
}