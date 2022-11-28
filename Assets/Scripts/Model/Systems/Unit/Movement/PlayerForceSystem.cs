using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents;
using Model.Components.Unit.MoveComponents.Input;
using UnityEngine;
using Zenject;

namespace Model.Systems.Unit.Movement
{
    public sealed class PlayerForceSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputMoveData, TransformData, Mass>.Exclude<NoEnergyBlock> _playerMove = null;
        [Inject] private Settings _settings;

        public void Run()
        {
            foreach (var i in _playerMove)
            {
                ref var inputData = ref _playerMove.Get1(i);
                ref var transform = ref _playerMove.Get2(i);
                ref var mass = ref _playerMove.Get3(i);
                ref var entity = ref _playerMove.GetEntity(i);
                
                if (inputData.Accelerate)
                {
                    Accelerate(ref entity, _settings.MoveForce, transform.LookDir, mass.Value);
                }
            }
        }

        private static void Accelerate(ref EcsEntity player, in float force, Vector2 direction, in float mass)
        {
            player.Get<ForceRequest>().Force += direction * (force / mass);
        }

        [Serializable]
        public class Settings
        {
            public float MoveForce;
        }
    }
}