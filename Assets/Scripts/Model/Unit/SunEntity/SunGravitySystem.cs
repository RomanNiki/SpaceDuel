using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Movement.Components;
using Model.Unit.Movement.Components.Tags;
using Model.Unit.SunEntity.Components;
using UnityEngine;

namespace Model.Unit.SunEntity
{
    public sealed class SunGravitySystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<Position, Velocity>.Exclude<GravityResist, Sun> _movableFilter = null;
        private readonly EcsFilter<Sun, Position> _sunFilter = null;

        protected override void Tick()
        {
            foreach (var j in _sunFilter)
            {
                ref var sun = ref _sunFilter.Get1(j);
                ref var sunPosition = ref _sunFilter.Get2(j);
                foreach (var i in _movableFilter)
                {
                    ref var objectPosition = ref _movableFilter.Get1(i);
                    ref var entity = ref _movableFilter.GetEntity(i);
                    var targetDirection = sunPosition.Value - objectPosition.Value;
                    var distance = targetDirection.magnitude;

                    if (distance <= 0.5f)
                        continue;
                    var force = targetDirection.normalized * sun.GravityForce ;
                    var distRatio = Mathf.Clamp01(distance / sun.OuterRadius);
                    
                    force *= 1.0f + distRatio;
                    entity.Get<ForceRequest>().Force += force * Time.deltaTime;
                }
            }
        }
    }
}