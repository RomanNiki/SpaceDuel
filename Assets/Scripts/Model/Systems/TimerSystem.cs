using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using Model.Pause;
using Model.Timers;
using UnityEngine;

namespace Model.Systems
{
    public sealed class TimerSystem<TTimerFlag> : IEcsRunSystem, IPauseHandler
        where TTimerFlag : struct
    {
        private readonly EcsFilter<Timer<TTimerFlag>>.Exclude<UnPause> _pauseFilter = null;
        private readonly EcsFilter<Timer<TTimerFlag>, UnPause> _filter = null;
        private readonly EcsFilter<GameRestartEvent> _restart = null;
        private bool _pause;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var timer = ref _filter.Get1(i);
                ref var entity = ref _filter.GetEntity(i);
                ExecuteTimer(ref timer, entity);
            }
           
            if (_pause || _restart.IsEmpty() == false)
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