using Components.Extensions;
using Components.Timers;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public sealed class TimerSystem<TTimerFlag> : IEcsRunSystem
        where TTimerFlag : struct
    {
        private readonly EcsFilter<Timer<TTimerFlag>> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var timer = ref _filter.Get1(i);
                timer.TimeLeftSec -= Time.deltaTime;

                if (timer.TimeLeftSec <= 0)
                {
                    _filter.GetEntity(i).Del<Timer<TTimerFlag>>();
                }
            }
        }
    }
}