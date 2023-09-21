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

    public sealed class FrictionSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<Friction> _frictionPool;
        private Stash<Velocity> _velocityPool;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Velocity>().With<Friction>().Build();
            _frictionPool = World.GetStash<Friction>();
            _velocityPool = World.GetStash<Velocity>();
        }

        public void OnUpdate(float deltaTime)
        {
#if MORPEH_BURST
            var filter = _filter.AsNative();
            var job = new FrictionJob()
            {
                FrictionComponents = _frictionPool.AsNative(),
                VelocityComponents = _velocityPool.AsNative(),
                Entities = filter
            };

            World.JobHandle = job.Schedule(filter.length, 64, World.JobHandle);
            World.JobsComplete();
#else
            foreach (var entity in _filter)
            {
                ref var friction = ref _frictionPool.Get(entity);
                ref var velocity = ref _velocityPool.Get(entity);

                var frictionDirection = -velocity.Value.normalized;
                var frictionMagnitude = velocity.Value.magnitude * velocity.Value.magnitude;
                var frictionVector = frictionMagnitude * frictionDirection;
                velocity.Value += friction.Value * frictionVector;
            }
#endif
        }

        public void Dispose()
        {
        }

#if MORPEH_BURST
        
        [BurstCompile]
        private struct FrictionJob : IJobParallelFor
        {
            [ReadOnly] public NativeFilter Entities;
            public NativeStash<Friction> FrictionComponents;
            public NativeStash<Velocity> VelocityComponents;
        
            public void Execute(int index)
            {
                var entityId = Entities[index];

                ref var friction = ref FrictionComponents.Get(entityId, out var forceRequestExists);
                ref var velocity = ref VelocityComponents.Get(entityId, out var velocityExists);
                if (velocityExists && forceRequestExists)
                {
                    var frictionDirection = -velocity.Value.normalized;
                    var frictionMagnitude = velocity.Value.magnitude * velocity.Value.magnitude;
                    var frictionVector = frictionMagnitude * frictionDirection;
                    velocity.Value += friction.Value * frictionVector;
                }
            }
        }
#endif
    }
}