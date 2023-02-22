using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using Model.Components.Requests;
using UnityEngine.VFX;
using Views.Extensions;


namespace Views.Systems
{
    public sealed class EffectPauseSystem : IEcsRunSystem
    {
        private EcsFilter<UnityComponent<EffectInteractor>> _filter;
        private EcsFilter<PauseRequest> _pause;
        private EcsFilter<StartGameRequest> _start;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var visualEffect = ref _filter.Get1(i);
                if (_pause.IsEmpty() == false)
                {
                    visualEffect.Value.SetPause(true);
                }

                if (_start.IsEmpty() == false)
                {
                    visualEffect.Value.SetPause(false);
                }
            }
        }
    }
}