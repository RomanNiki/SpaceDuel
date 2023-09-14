using Core.Effects.Components;
using Core.Weapon.Components;

namespace Core.Effects.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class HitEffectSystem : DestroyEffectCreateSystem<BulletTag, HitTag>
    {
    }
}