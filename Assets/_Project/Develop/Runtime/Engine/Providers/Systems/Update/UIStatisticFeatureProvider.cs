using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using _Project.Develop.Runtime.Engine.UI.Statistics;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(UIStatisticFeature))]
    public sealed class UIStatisticFeatureProvider : UpdateFeatureProvider<UIStatisticFeature>
    {}
}