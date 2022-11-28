using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Systems.Unit.Movement
{
    public sealed class SunGravitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformData>.Exclude<NoGravity, Sun> _move = null;
        private readonly EcsFilter<Sun, TransformData> _sun = null;

        public void Run()
        {
            foreach (var j in _sun)
            {
                ref var sun = ref _sun.Get1(j);
                ref var sunTransform = ref _sun.Get2(j);
                foreach (var i in _move)
                {
                    ref var transform = ref _move.Get1(i);
                    ref var entity = ref _move.GetEntity(i);
                    var targetDirection = sunTransform.Position - transform.Position;
                    var distance = targetDirection.magnitude;

                    if (distance <= 0.5f)
                        return;
                    var force = targetDirection.normalized * sun.GravityForce ;
                    var distRatio = Mathf.Clamp01(distance / sun.Radius);
                    
                    force *= 1.0f + distRatio;
                    entity.Get<ForceRequest>().Force += force * Time.deltaTime;
                }
            }
        }
    }
}