using Core.Movement.Components;
using Scellecs.Morpeh;

#if MORPEH_BURST
using Scellecs.Morpeh.Native;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
#endif

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
#if MORPEH_BURST
            var filter = _filter.AsNative();
            var job = new AccelerateJobReference()
            {
                Entities = filter,
                VelocityComponents = _velocityPool.AsNative(),
                ForceRequestComponents = _forceRequestPool.AsNative()
            };
            World.JobHandle = job.Schedule(filter.length, 64, World.JobHandle);
            World.JobsComplete();
#else
            foreach (var entity in _filter)
            {
                ref var forceRequest = ref _forceRequestPool.Get(entity);
                ref var velocityEntity = ref forceRequest.Entity;
                ref var velocity = ref _velocityPool.Get(velocityEntity);

                velocity.Value += forceRequest.Value;
            }
#endif
            _forceRequestPool.RemoveAll();
        }

        public void Dispose()
        {
        }

#if MORPEH_BURST
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
#endif
    }
}