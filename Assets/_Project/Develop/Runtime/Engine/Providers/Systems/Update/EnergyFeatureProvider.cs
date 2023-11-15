using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(EnergyFeature))]
    public class EnergyFeatureProvider : UpdateFeatureProvider<EnergyFeature>
    {}
}