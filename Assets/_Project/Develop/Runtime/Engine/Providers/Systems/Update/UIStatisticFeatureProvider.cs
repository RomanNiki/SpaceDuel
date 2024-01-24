using _Project.Develop.Runtime.Engine.ECS.UI.PlayerUI;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(UIStatisticFeature))]
    public sealed class UIStatisticFeatureProvider : UpdateFeatureProvider<UIStatisticFeature>
    {}
}