using UnityEngine.Serialization;

namespace Core.Collisions.Components
{
    using System;
    using Scellecs.Morpeh;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    [Serializable]
    public struct TriggerEnterRequest : IComponent
    {
        public Entity Sender;
        public Entity Target;

        public TriggerEnterRequest(Entity sender, Entity target)
        {
            Sender = sender;
            Target = target;
        }
    }
}