using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Converters.Base
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public class Converter<TComponent> : ConverterBase where TComponent : struct, IComponent
    {
        [SerializeField] private TComponent _component;

        public override void Resolve(World world, Entity entity)
        {
            world.GetStash<TComponent>().Set(entity, _component);
        }
    }
}