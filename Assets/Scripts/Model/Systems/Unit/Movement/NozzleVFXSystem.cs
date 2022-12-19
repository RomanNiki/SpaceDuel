using Leopotam.Ecs;
using Model.Components;
using Model.Components.Unit.MoveComponents.Input;
using UnityEngine.VFX;

namespace Model.Systems.Unit.Movement
{
    public sealed class NozzleVFXSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputMoveData, UnityComponent<VisualEffect>>.Exclude<NoEnergyBlock> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var particleSystem = ref _filter.Get2(i);

                if (input.Accelerate == false)
                {
                    particleSystem.Value.Stop(); 
                    continue;
                }
                particleSystem.Value.Play();
            }
        }
    }
}