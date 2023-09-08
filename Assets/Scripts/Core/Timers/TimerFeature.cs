using Core.Extensions;
using Core.Timers.Components;
using Core.Timers.Systems;
using Cysharp.Threading.Tasks;

namespace Core.Timers
{
    public class TimerFeature : BaseMorpehFeature
    {
        protected async override UniTask InitializeSystems()
        {
            AddSystem(new LifeCycleSystem());
            AddSystem(new TimerSystem<TimerBetweenShots>());
            AddSystem(new TimerSystem<LifeTimer>());
            AddSystem(new TimerSystem<TimerBetweenSpawn>());
            AddSystem(new TimerSystem<InvisibleTimer>());
        }
    }
}