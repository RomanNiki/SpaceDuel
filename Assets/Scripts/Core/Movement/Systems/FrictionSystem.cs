using Core.Movement.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Native;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

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
            var filter = _filter.AsNative();
            var job = new FrictionJob()
            {
                FrictionComponents = _frictionPool.AsNative(),
                VelocityComponents = _velocityPool.AsNative(),
                Entities = filter
            };

            var handler = job.Schedule(filter.length, 64);
            handler.Complete();
        }
        
        public void Dispose()
        {
        }
        
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
    }
}