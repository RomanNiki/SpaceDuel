using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using _Project.Develop.Runtime.Engine.Sounds;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(SoundFeature))]
    public sealed class SoundFeatureProvider : UpdateFeatureProvider<SoundFeature> 
    {}
}