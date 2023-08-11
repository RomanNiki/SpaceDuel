using Core.Movement.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Native;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Core.Movement.Systems
{
    public sealed class AccelerationSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<ForceRequest> _forceRequestPool;
        private Stash<Velocity> _velocityPool;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Velocity>().With<ForceRequest>();
            _forceRequestPool = World.GetStash<ForceRequest>();
            _velocityPool = World.GetStash<Velocity>();
        }

        public void OnUpdate(float deltaTime)
        {
            using var filter = _filter.AsNative();
            var job = new AccelerateJobReference()
            {
                Entities = filter,
                VelocityComponents = _velocityPool.AsNative(),
                ForceRequestComponents = _forceRequestPool.AsNative()
            };

            var handler = job.Schedule(filter.length, 64);
            handler.Complete();
        }

        public void Dispose()
        {
        }
    }

    [BurstCompile]
    public struct AccelerateJobReference : IJobParallelFor
    {
        [ReadOnly] public NativeFilter Entities;
        public NativeStash<ForceRequest> ForceRequestComponents;
        public NativeStash<Velocity> VelocityComponents;

        public void Execute(int index)
        {
            var entityId = Entities[index];

            ref var forceRequest = ref ForceRequestComponents.Get(entityId, out var forceRequestExists);
            ref var velocity = ref VelocityComponents.Get(entityId, out var velocityExists);
            if (velocityExists && forceRequestExists)
            {
                velocity.Value += forceRequest.Value;
                forceRequest.Value = Vector2.zero;
            }
        }
    }
}