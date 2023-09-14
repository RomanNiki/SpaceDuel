using Core.Movement.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Native;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Core.Movement.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class AccelerationSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<ForceRequest> _forceRequestPool;
        private Stash<Velocity> _velocityPool;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<ForceRequest>().Build();
            _forceRequestPool = World.GetStash<ForceRequest>();
            _velocityPool = World.GetStash<Velocity>();
        }

        public void OnUpdate(float deltaTime)
        {
            var filter = _filter.AsNative();
            var job = new AccelerateJobReference()
            {
                Entities = filter,
                VelocityComponents = _velocityPool.AsNative(),
                ForceRequestComponents = _forceRequestPool.AsNative()
            };
            var handler = job.Schedule(filter.length, 64);
            handler.Complete();
            _forceRequestPool.RemoveAll();
        }

        public void Dispose()
        {
        }

        [BurstCompile]
        private struct AccelerateJobReference : IJobParallelFor
        {
            [ReadOnly] public NativeFilter Entities;
            public NativeStash<ForceRequest> ForceRequestComponents;
            public NativeStash<Velocity> VelocityComponents;

            public void Execute(int index)
            {
                var entityId = Entities[index];
                ref var forceRequest = ref ForceRequestComponents.Get(entityId, out var forceRequestExists);
                ref var velocityEntity = ref forceRequest.EntityId;
                ref var velocity = ref VelocityComponents.Get(velocityEntity, out var velocityExists);
                if (velocityExists && forceRequestExists)
                {
                    velocity.Value += forceRequest.Value;
                }
            }
        }
    }
}