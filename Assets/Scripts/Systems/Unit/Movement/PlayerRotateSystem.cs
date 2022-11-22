using System;
using Components;
using Components.Unit.MoveComponents;
using Components.Unit.MoveComponents.Input;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Systems.Unit.Movement
{
    public sealed class PlayerRotateSystem : IEcsRunSystem
    {
        [Inject] private Settings _settings;
        private readonly EcsFilter<InputMoveData, Move>.Exclude<NoEnergyBlock> _playerMove = null;
        
        public void Run()
        {
            foreach (var i in _playerMove)
            {
                ref var inputData = ref _playerMove.Get1(i);
                ref var moveComponent = ref _playerMove.Get2(i);
                if (MathF.Abs(inputData.Rotation) < 0.1f)
                    continue;

                Rotate(ref moveComponent, inputData.Rotation * (_settings.RotationSpeed * Time.fixedDeltaTime));
            }
        }
        
        private static void Rotate(ref Move move, float delta)
        {
            move.Rotation = Mathf.Repeat(move.Rotation + delta, 360f);
        }
        
        [Serializable]
        public class Settings
        {
            public float RotationSpeed;
        }
    }
}