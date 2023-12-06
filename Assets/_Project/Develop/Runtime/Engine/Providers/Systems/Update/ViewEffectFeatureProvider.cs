using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using _Project.Develop.Runtime.Engine.Views.Effects;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(ViewEffectFeature))]
    public sealed class ViewEffectFeatureProvider : UpdateFeatureProvider<ViewEffectFeature>
    {
    }
}