using _Project.Develop.Runtime.Core.Init;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(InitFeature))]
    public sealed class InitFeatureProvider : UpdateFeatureProvider<InitFeature>
    {}
}