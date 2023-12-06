using _Project.Develop.Runtime.Core.Weapon;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(WeaponFeature))]
    public sealed class WeaponFeatureProvider : UpdateFeatureProvider<WeaponFeature>
    {}
}