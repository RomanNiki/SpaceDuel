using Core.Common.Enums;
using Core.Effects.Components;

namespace Core.Effects.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class ExplosionCreateSystem : DestroyEffectCreateSystem<ExplosiveTag, ExplosionTag>
    {
        protected override ObjectId GetObjectId() => ObjectId.Explosion;
    }
}