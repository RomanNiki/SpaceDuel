using Core.Timers.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Timers.Systems
{
    public class TimerSystem<TTimerFlag> : ISystem where TTimerFlag : struct, IComponent
    {
        private Filter _filter;
        private Stash<Timer<TTimerFlag>> _timerPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<Timer<TTimerFlag>>();
            _timerPool = World.GetStash<Timer<TTimerFlag>>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var timer = ref _timerPool.Get(entity);
                ExecuteTimer(ref timer, entity);
            }
        }
        
        private void ExecuteTimer(ref Timer<TTimerFlag> timer, in Entity entity)
        {
            timer.TimeLeftSec -= Time.deltaTime;

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