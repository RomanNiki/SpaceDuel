using _Project.Develop.Runtime.Core.Movement.Components.Gravity;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Movement
{
    public class GravityPointMonoProvider : MonoProvider<GravityPoint>
    {
        private void OnDrawGizmos()
        {
            var position = transform.position;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(position, Data.OuterRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, Data.InnerRadius);
        }
    }
}