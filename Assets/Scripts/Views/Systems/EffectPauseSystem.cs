using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using Model.Components.Requests;
using Views.Extensions;

namespace Views.Systems
{
    public sealed class EffectPauseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnityComponent<EffectInteractor>> _filter;
        private readonly EcsFilter<PauseRequest> _pause;
        private readonly EcsFilter<StartGameRequest> _start;

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