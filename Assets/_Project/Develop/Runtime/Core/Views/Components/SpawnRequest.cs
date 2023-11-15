using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Views.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public readonly struct SpawnRequest : IComponent
    {
        public readonly Entity Entity;
        public readonly Vector2 Position;
        public readonly float Rotation;
        public readonly ObjectId Id;

        public SpawnRequest(Entity entity, ObjectId id, Vector2 position, float rotation)
        {
            Entity = entity;
            Id = id;
            Position = position;
            Rotation = rotation;
        }
    }
}