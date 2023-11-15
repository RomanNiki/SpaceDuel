using _Project.Develop.Runtime.Core.Timers;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(TimerFeature))]
    public class TimerFeatureProvider : UpdateFeatureProvider<TimerFeature>
    {}
}