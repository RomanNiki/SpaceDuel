using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(InputFeature))]
    public sealed class InputFeatureProvider : UpdateFeatureProvider<InputFeature>
    {
    }
}