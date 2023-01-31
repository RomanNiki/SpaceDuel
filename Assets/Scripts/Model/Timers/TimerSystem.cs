using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using Model.Extensions.Pause;
using Model.Timers.Components;
using UnityEngine;

namespace Model.Timers
{
    public sealed class TimerSystem<TTimerFlag> : IEcsRunSystem, IPauseHandler
        where TTimerFlag : struct
    {
        private readonly EcsFilter<Timer<TTimerFlag>>.Exclude<UnPause> _pauseFilter = null;
        private readonly EcsFilter<Timer<TTimerFlag>, UnPause> _unpauseFilter = null;
        private readonly EcsFilter<GameRestartEvent> _restartFilter = null;
        private bool _pause;

        public void Run()
        {
            foreach (var i in _unpauseFilter)
            {
                ref var timer = ref _unpauseFilter.Get1(i);
                ref var entity = ref _unpauseFilter.GetEntity(i);
                ExecuteTimer(ref timer, entity);
            }
           
            if (_pause || _restartFilter.IsEmpty() == false)
                return;

            foreach (var i in _pauseFilter)
            {
                ref var timer = ref _pauseFilter.Get1(i);
                ref var entity = ref _pauseFilter.GetEntity(i);
                ExecuteTimer(ref timer, entity);
            }
    
        }

        private static void ExecuteTimer(ref Timer<TTimerFlag> timer, in EcsEntity entity)
        {
            timer.TimeLeftSec -= Time.deltaTime;

            if (timer.TimeLeftSec <= 0)
            {
                entity.Del<Timer<TTimerFlag>>();
            }
        }
            
        public void SetPaused(bool isPaused)
        {
            _pause = isPaused;
        }
    }
}