using _Project.Develop.Runtime.Core.Timers.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Timers.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class TimerSystem<TTimerFlag> : ISystem where TTimerFlag : struct, IComponent
    {
        private Filter _filter;
        private Stash<Timer<TTimerFlag>> _timerPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Timer<TTimerFlag>>().Build();
            _timerPool = World.GetStash<Timer<TTimerFlag>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var timer = ref _timerPool.Get(entity);
                ExecuteTimer(ref timer, entity, deltaTime);
            }
        }

        private void ExecuteTimer(ref Timer<TTimerFlag> timer, Entity entity, float delta)
        {
            timer.TimeLeftSec -= delta;

            if (timer.TimeLeftSec <= 0)
            {
                _timerPool.Remove(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}