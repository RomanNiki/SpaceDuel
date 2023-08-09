using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Input.Components;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Model.Unit.Movement
{
    public sealed class PlayerForceSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<InputMoveData, Rotation, Mass, Velocity, Speed>.Exclude<NoEnergyBlock> _playerMoveFilter = null;

        protected override void Tick()
        {
            foreach (var i in _playerMoveFilter)
            {
                ref var inputData = ref _playerMoveFilter.Get1(i);
                ref var rotation = ref _playerMoveFilter.Get2(i);
                ref var mass = ref _playerMoveFilter.Get3(i);
                ref var speed = ref _playerMoveFilter.Get5(i);
                ref var entity = ref _playerMoveFilter.GetEntity(i);

                if (inputData.Accelerate)
                {
                    Accelerate(ref entity, speed.Value, rotation.LookDir, mass.Value);
                }
            }
        }

        private static void Accelerate(ref EcsEntity player, in float force, Vector2 direction, in float mass)
        {
            player.Get<ForceRequest>().Force += direction * (force / mass);
        }
    }
}