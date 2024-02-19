using _Project.Develop.Runtime.Core.Buffs.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Buffs
{
    public class SpawnRadiusMonoProvider : MonoProvider<SpawnRadius>
    {
        private void OnDrawGizmos()
        {
            var position = transform.position;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, Data.OuterRadius);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(position, Data.InnerRadius);
        }
    }
}