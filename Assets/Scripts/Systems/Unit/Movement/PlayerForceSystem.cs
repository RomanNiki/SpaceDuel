using System;
using Components;
using Components.Unit.MoveComponents;
using Components.Unit.MoveComponents.Input;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Systems.Unit.Movement
{
    public sealed class PlayerForceSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputMoveData, Move, Mass>.Exclude<NoEnergyBlock> _playerMove = null;
        [Inject] private Settings _settings;

        public void Run()
        {
            foreach (var i in _playerMove)
            {
                ref var inputData = ref _playerMove.Get1(i);
                ref var moveComponent = ref _playerMove.Get2(i);
                ref var mass = ref _playerMove.Get3(i);
                if (inputData.Accelerate)
                {
                    Accelerate(ref moveComponent, _settings.MoveForce, mass.Value);
                }
                else
                {
                    moveComponent.Acceleration = Vector2.zero;
                }
            }
        }

        private static void Accelerate(ref Move move, in float force, in float mass)
        {
            move.Acceleration = move.LookDir * (force / mass);
        }

        [Serializable]
        public class Settings
        {
            public float MoveForce;
        }
    }
}