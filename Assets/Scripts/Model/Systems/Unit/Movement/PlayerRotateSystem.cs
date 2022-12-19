using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Unit.MoveComponents;
using Model.Components.Unit.MoveComponents.Input;
using UnityEngine;
using Zenject;

namespace Model.Systems.Unit.Movement
{
    public sealed class PlayerRotateSystem : IEcsRunSystem
    {
        [Inject] private Settings _settings;
        private readonly EcsFilter<InputMoveData, Rotation>.Exclude<NoEnergyBlock> _playerMove = null;
        
        public void Run()
        {
            foreach (var i in _playerMove)
            {
                ref var inputData = ref _playerMove.Get1(i);
                ref var rotation = ref _playerMove.Get2(i);
                if (MathF.Abs(inputData.Rotation) < 0.1f)
                    continue;

                Rotate(ref rotation, inputData.Rotation * (_settings.RotationSpeed * Time.deltaTime));
            }
        }
        
        private static void Rotate(ref Rotation rotation, float delta)
        {
            rotation.Value = Mathf.Repeat(rotation.Value + delta, 360f);
        }
        
        [Serializable]
        public class Settings
        {
            public float RotationSpeed;
        }
    }
}