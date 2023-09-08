using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Views.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public struct ViewCreateRequest : IComponent
    {
        public Entity Entity;
        public Vector2 Position;
        public float Rotation;

        public ViewCreateRequest(Entity entity, Vector2 position, float rotation)
        {
            Entity = entity;
            Position = position;
            Rotation = rotation;
        }
    }
}