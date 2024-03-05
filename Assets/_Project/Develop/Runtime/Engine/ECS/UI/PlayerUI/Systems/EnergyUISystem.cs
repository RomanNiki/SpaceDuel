using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Engine.UI.Statistics.Sliders;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.ECS.UI.PlayerUI.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class EnergyUISystem : UISliderUpdateSystem<EnergyChangedEvent, Energy, EnergySlider>
    {
        protected override ref Entity GetEventOwner(Stash<EnergyChangedEvent> eventStash, Entity eventEntity) =>
            ref eventStash.Get(eventEntity).Entity;
    }
}