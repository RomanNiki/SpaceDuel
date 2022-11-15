using System;
using Components;
using Components.Player.MoveComponents;
using Leopotam.Ecs;
using Models.Player;
using UnityEngine;
using Zenject;

namespace Systems.Player.Movement
{
    public sealed class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputData, Move>.Exclude<NoEnergyBlock> _playerMove = null;
        [Inject] private PlayerMover.Settings _settings;
         
        public void Run()
        {

            foreach (var i in _playerMove)
            {
                Debug.Log(i);
                ref var inputData  = ref _playerMove.Get1(i);
                ref var moveComponent = ref _playerMove.Get2(i);
                if (inputData.Accelerate)
                {
                    moveComponent.ViewObject.AddForce(moveComponent.LookDir * _settings.MoveSpeed);
                }

                if (MathF.Abs(inputData.Rotation) < 0.1f)
                    continue;

                Rotate(ref moveComponent ,inputData.Rotation * (_settings.RotationSpeed * Time.fixedDeltaTime));
            }
        }
        
        private static void Rotate(ref Move move, float delta)
        {
            move.ViewObject.Rotation = Mathf.Repeat(move.ViewObject.Rotation + delta, 360f);
        }
    }
}