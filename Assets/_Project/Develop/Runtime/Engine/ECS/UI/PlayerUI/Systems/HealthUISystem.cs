using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Engine.UI.Statistics.Services;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.ECS.UI.PlayerUI.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class HealthUISystem : UISliderUpdateSystem<HealthChangedEvent, Health, HealthSlider>
    {
        protected override ref Entity GetEventOwner(Stash<HealthChangedEvent> eventStash, Entity eventEntity) =>
            ref eventStash.Get(eventEntity).Entity;
    }
}