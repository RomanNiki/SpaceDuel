using Leopotam.Ecs;
using Model.Components;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents.Input;
using Model.Pause;
using UnityEngine.VFX;

namespace Views.Systems.Update
{
    public sealed class NozzleVFXSystem : IEcsRunSystem, IPauseHandler
    {
        private readonly EcsFilter<InputMoveData, Nozzle, UnityComponent<VisualEffect>>.Exclude<NoEnergyBlock> _filter;
        private bool _isPause;

        public void Run()
        {
            if (_isPause)
                return;
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var nozzle = ref _filter.Get2(i);
                ref var visualEffect = ref _filter.Get3(i);

                if (input.Accelerate == false)
                {
                    if (nozzle.Active)
                    {
                        visualEffect.Value.Stop();
                    }

                    continue;
                }

                visualEffect.Value.Play();
                nozzle.Active = true;
            }
        }

        public void SetPaused(bool isPaused)
        {
            _isPause = isPaused;
            foreach (var i in _filter)
            {
                ref var visualEffect = ref _filter.Get3(i);
                if (isPaused)
                {
                    visualEffect.Value.Stop();
                }
                else
                {
                    ref var nozzle = ref _filter.Get2(i);
                    if (nozzle.Active)
                    {
                        visualEffect.Value.Play();
                    }
                }
            }
        }
    }
}