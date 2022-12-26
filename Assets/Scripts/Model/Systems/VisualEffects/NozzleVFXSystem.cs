using Leopotam.Ecs;
using Model.Components;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents.Input;
using UnityEngine.VFX;

namespace Model.Systems.VisualEffects
{
    public sealed class NozzleVFXSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputMoveData, Nozzle>.Exclude<NoEnergyBlock> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var nozzle = ref _filter.Get2(i);

                if (input.Accelerate == false)
                {
                    if (nozzle.Active)
                    {
                        nozzle.VisualEffect.Stop();
                    }
                    continue;
                }
                nozzle.VisualEffect.Play();
                nozzle.Active = true;
            }
        }
    }
}