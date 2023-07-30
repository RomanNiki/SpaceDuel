using System;
using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Input.Components;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Model.Unit.Movement
{
    public sealed class PlayerRotateSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<InputMoveData, Rotation, RotationSpeed>.Exclude<NoEnergyBlock> _playerRotateFilter = null;
        
        protected override void Tick()
        {
            foreach (var i in _playerRotateFilter)
            {
                ref var inputData = ref _playerRotateFilter.Get1(i);
              
                if (MathF.Abs(inputData.Rotation) < 0.1f)
                    continue;
                ref var rotation = ref _playerRotateFilter.Get2(i);
                ref var rotationSpeed = ref _playerRotateFilter.Get3(i);
                Rotate(ref rotation, inputData.Rotation * (rotationSpeed.Value * Time.deltaTime));
            }
        }

        private static void Rotate(ref Rotation rotation, float delta)
        {
            rotation.Value = Mathf.Repeat(rotation.Value + delta, 360f);
        }
    }
}