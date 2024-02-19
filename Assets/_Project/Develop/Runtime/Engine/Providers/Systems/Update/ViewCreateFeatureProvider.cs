using _Project.Develop.Runtime.Core.Views;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(ViewCreateFeature))]
    public sealed class ViewCreateFeatureProvider : UpdateFeatureProvider<ViewCreateFeature>
    {
    }
}