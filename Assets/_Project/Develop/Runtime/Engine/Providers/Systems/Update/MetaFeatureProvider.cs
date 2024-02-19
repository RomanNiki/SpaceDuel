using _Project.Develop.Runtime.Core.Meta;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(MetaFeature))]
    public sealed class MetaFeatureProvider : UpdateFeatureProvider<MetaFeature>
    {
    }
}