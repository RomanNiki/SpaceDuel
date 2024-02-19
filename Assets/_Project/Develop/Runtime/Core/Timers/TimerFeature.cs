using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Core.Timers.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Timers
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