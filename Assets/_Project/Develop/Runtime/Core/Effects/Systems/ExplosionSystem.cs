using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Effects.Components;

namespace _Project.Develop.Runtime.Core.Effects.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class ExplosionSystem : CreateDestroyEffectSystem<ExplosiveTag, ExplosionTag>
    {
        protected override ObjectId GetObjectId() => ObjectId.Explosion;
    }
}