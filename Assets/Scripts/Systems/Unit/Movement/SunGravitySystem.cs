using Components;
using Components.Tags;
using Components.Unit.MoveComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Unit.Movement
{
    public sealed class SunGravitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Move>.Exclude<NoGravity> _move = null;
        private readonly EcsFilter<Sun> _sun = null;
        private const float G = 667.4f;

        public void Run()
        {
            foreach (var j in _sun)
            {
                ref var sun = ref _sun.Get1(j);
                foreach (var i in _move)
                {
                    ref var move = ref _move.Get1(i);

                    var targetDirection = sun.Position - move.Position;
                    var distance = targetDirection.magnitude;

                    if (distance <= 0.5f)
                        return;
                    var forceMagnitude = G * sun.GravityForce / Mathf.Pow(distance, 2);
                    var force = targetDirection.normalized * forceMagnitude;
                    move.Velocity += force * Time.deltaTime;
                }
            }
        }
    }
}