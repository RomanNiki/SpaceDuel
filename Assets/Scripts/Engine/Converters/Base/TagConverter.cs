using System;
using Scellecs.Morpeh;

namespace Engine.Converters.Base
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public class TagConverter<TTag> : ConverterBase where TTag : struct, IComponent
    {
        public override void Resolve(World world, Entity entity)
        {
            world.GetStash<TTag>().Set(entity);
        }
    }
}