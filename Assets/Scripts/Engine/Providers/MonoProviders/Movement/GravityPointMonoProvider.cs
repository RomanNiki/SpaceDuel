using System;
using Core.Movement.Gravity.Components;
using Engine.Providers.MonoProviders.Base;
using UnityEngine;

namespace Engine.Providers.MonoProviders.Movement
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
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