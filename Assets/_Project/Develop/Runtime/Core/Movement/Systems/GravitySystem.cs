using System;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Movement.Components.Gravity;
using Scellecs.Morpeh;
using UnityEngine;
#if UNITY_WEBGL == false
using Scellecs.Morpeh.Native;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
#endif

namespace _Project.Develop.Runtime.Core.Movement.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public sealed class GravitySystem : IFixedSystem
    {
        private const float G = 0.06674f; //6,7 * 10^-11
        private Filter _gravityFilter;
        private Filter _movableFilter;
        private Stash<Position> _positionPool;
        private Stash<Mass> _massPool;
        private Stash<GravityPoint> _gravityPoint;

        public World World { get; set; }

        public void OnAwake()
        {
            _gravityFilter = World.Filter.With<GravityPoint>().With<Position>().Build();
            _movableFilter = World.Filter.With<Position>().With<Velocity>().Without<GravityResistTag>().Build();
            _positionPool = World.GetStash<Position>();
            _massPool = World.GetStash<Mass>();
            _gravityPoint = World.GetStash<GravityPoint>();
        }

        public void OnUpdate(float deltaTime)
        {
#if UNITY_WEBGL
            foreach (var gravityPointEntity in _gravityFilter)
            {
                ref var gravityPointPosition = ref _positionPool.Get(gravityPointEntity);
                ref var pointMass = ref _massPool.Get(gravityPointEntity);
                ref var gravityPoint = ref _gravityPoint.Get(gravityPointEntity);

                foreach (var movableEntity in _movableFilter)
                {
                    ref var position = ref _positionPool.Get(movableEntity);
                    ref var mass = ref _massPool.Get(movableEntity);
                    var targetDirection = gravityPointPosition.Value - position.Value;
                    var distance = targetDirection.magnitude;

                    if (IsInBounds(distance, gravityPoint))
                        continue;

                    var massProduct = pointMass.Value * mass.Value * G;
                    var force = CalculateForce(distance, gravityPoint, massProduct, targetDirection, mass);

                    World.SendMessage(new ForceRequest { Value = force, Entity = movableEntity });
                }
            }
#else
            var gravityPointsFilter = _gravityFilter.AsNative();
            var movableFilter = _movableFilter.AsNative();
            var length = gravityPointsFilter.length * movableFilter.length;
            var forceRequests =
                new NativeArray<ForceRequest>(length, Allocator.TempJob);
            var job = new GravityJob()
            {
                GravityEntities = gravityPointsFilter,
                MovableEntities = movableFilter,
                ForceRequests = forceRequests,
                MassComponents = _massPool.AsNative(),
                PositionComponents = _positionPool.AsNative(),
                GravityPointComponents = _gravityPoint.AsNative()
            };

            World.JobHandle = job.Schedule(length, 64, World.JobHandle);
            World.JobsComplete();
            foreach (var forceRequest in forceRequests)
            {
                World.SendMessage(forceRequest);
            }

            forceRequests.Dispose();
#endif
        }


        private static Vector2 CalculateForce(in float distance, in GravityPoint gravityPoint, in float massProduct,
            in Vector2 targetDirection, in Mass mass)
        {
            var radiusRatio = 1 - Mathf.Clamp01(distance / gravityPoint.OuterRadius);
            var unscaledForceMagnitude = massProduct * radiusRatio / Mathf.Pow(distance, 2);
            var forceMagnitude = G * unscaledForceMagnitude;
            var forceDirection = targetDirection.normalized;
            var force = forceDirection * forceMagnitude / mass.Value;
            return force;
        }

        private static bool IsInBounds(float distance, in GravityPoint gravityPointComponent)
        {
            return distance <= gravityPointComponent.InnerRadius || distance >= gravityPointComponent.OuterRadius;
        }

        public void Dispose()
        {
        }

#if UNITY_WEBGL == false
        [BurstCompile]
        private struct GravityJob : IJobParallelFor
        {
            [ReadOnly] public NativeFilter MovableEntities;
            [ReadOnly] public NativeFilter GravityEntities;
            public NativeArray<ForceRequest> ForceRequests;
            public NativeStash<Position> PositionComponents;
            public NativeStash<Mass> MassComponents;
            public NativeStash<GravityPoint> GravityPointComponents;

            public void Execute(int index)
            {
                var gravityPointIndex = index / MovableEntities.length;
                var entityIndex = index % MovableEntities.length;
                var entityId = MovableEntities[entityIndex];
                var gravityId = GravityEntities[gravityPointIndex];
                ref var position = ref PositionComponents.Get(entityId);
                ref var mass = ref MassComponents.Get(entityId);
                var targetDirection = PositionComponents.Get(gravityId).Value - position.Value;
                var distance = targetDirection.magnitude;

                ref var gravityPointComponent = ref GravityPointComponents.Get(gravityId);
                if (IsInBounds(distance, gravityPointComponent))
                    return;

                var massProduct = MassComponents.Get(gravityId).Value * mass.Value * G;
                var force = CalculateForce(distance, gravityPointComponent, massProduct, targetDirection, mass);
                ForceRequests[index] = new ForceRequest { Value = force, EntityId = entityId };
            }
        }
#endif
    }
}