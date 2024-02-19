using _Project.Develop.Runtime.Core.Buffs;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(BuffFeature))]
    public sealed class BuffFeatureProvider : UpdateFeatureProvider<BuffFeature>
    {
    }
}