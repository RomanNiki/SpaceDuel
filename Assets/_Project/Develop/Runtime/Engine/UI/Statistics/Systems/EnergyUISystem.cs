using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Engine.UI.Statistics.Services;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.UI.Statistics.Systems
{
    public sealed class EnergyUISystem : UISliderUpdateSystem<EnergyChangedEvent, Energy, EnergySlider>
    {
        protected override ref Entity GetEventOwner(Stash<EnergyChangedEvent> eventStash, Entity eventEntity) =>
            ref eventStash.Get(eventEntity).Entity;
    }
}