using Core.Timers.Components;
using Core.Timers.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Timers
{
    public class TimerFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new LifeCycleSystem());
            AddSystem(new TimerSystem<TimerBetweenShots>());
            AddSystem(new TimerSystem<LifeTimer>());
            AddSystem(new TimerSystem<TimerBetweenSpawn>());
            AddSystem(new TimerSystem<InvisibleTimer>());
        }
    }
}