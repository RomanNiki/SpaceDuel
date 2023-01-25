using System;
using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Input.Components;
using Model.Unit.Movement.Components;
using UnityEngine;
using Zenject;

namespace Model.Unit.Movement
{
    public sealed class PlayerForceSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<InputMoveData, Rotation, Mass, Velocity>.Exclude<NoEnergyBlock> _playerMove = null;
        [Inject] private Settings _settings;

        protected override void Tick()
        {
            foreach (var i in _playerMove)
            {
                ref var inputData = ref _playerMove.Get1(i);
                ref var rotation = ref _playerMove.Get2(i);
                ref var mass = ref _playerMove.Get3(i);
                ref var entity = ref _playerMove.GetEntity(i);

                if (inputData.Accelerate)
                {
                    Accelerate(ref entity, _settings.MoveForce, rotation.LookDir, mass.Value);
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