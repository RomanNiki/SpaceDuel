using _Project.Develop.Runtime.Core.Characteristics.Damage;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(DamageFeature))]
    public sealed class DamageFeatureProvider : UpdateFeatureProvider<DamageFeature>
    {}
}