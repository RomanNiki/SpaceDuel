using _Project.Develop.Runtime.Core.Effects;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(EffectEntityFeature))]
    public class EffectEntityFeatureProvider : UpdateFeatureProvider<EffectEntityFeature>
    {}
}