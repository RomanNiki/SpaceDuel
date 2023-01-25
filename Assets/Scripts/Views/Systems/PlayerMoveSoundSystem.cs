using Extensions.MappingUnityToModel;
using Leopotam.Ecs;
using Model.Components;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Input.Components;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Views.Systems
{
    public sealed class PlayerMoveSoundSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnityComponent<PlayerAudioComponent>, InputMoveData, Rotation>.Exclude<NoEnergyBlock> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var accelerate = ref _filter.Get2(i);
                ref var soundComponent = ref _filter.Get1(i);
                

                if (Mathf.Abs(_filter.Get2(i).Rotation) > 0.5f)
                {
                    soundComponent.Value.RotateSoundSound();
                }
                
                soundComponent.Value.AccelerateSound(accelerate.Accelerate);
            }
        }
    }
}