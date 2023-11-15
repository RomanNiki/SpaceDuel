using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct DamageRequest : IComponent
    {
        public Entity Entity;
        public Vector3 Position;
        public float Damage;

        public DamageRequest(float damage, Vector3 position, Entity entity)
        {
            Damage = damage;
            Position = position;
            Entity = entity;
        }
    }
}