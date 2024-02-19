using _Project.Develop.Runtime.Engine.ECS.Sounds;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(SoundFeature))]
    public sealed class SoundFeatureProvider : UpdateFeatureProvider<SoundFeature> 
    {}
}