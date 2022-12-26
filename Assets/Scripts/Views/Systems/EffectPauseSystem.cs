using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using UnityEngine.VFX;


namespace Views.Systems
{
    public class EffectPauseSystem : IEcsRunSystem
    {
        private EcsFilter<UnityComponent<VisualEffect>> _filter;
        private EcsFilter<PauseEvent> _pause;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var visualEffect = ref _filter.Get1(i);
                foreach (var j in _pause)
                {
                    visualEffect.Value.pause = _pause.Get1(j).Pause;
                }
            }
        }
    }
}