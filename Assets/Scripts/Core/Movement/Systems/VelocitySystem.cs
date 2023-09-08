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
    
    public sealed class VelocitySystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<Velocity> _velocityPool;
        private Stash<Position> _positionPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Velocity>().With<Position>().Build();
            _velocityPool = World.GetStash<Velocity>();
            _positionPool = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            var filter = _filter.AsNative();
            var handler = new VelocityJob()
            {
                PositionComponents = _positionPool.AsNative(),
                VelocityComponents = _velocityPool.AsNative(),
                Delta = deltaTime,
                Entities = filter
            }.Schedule(filter.length, 64);
            handler.Complete();
        }

        public void Dispose()
        {
        }

        [BurstCompile]
        private struct VelocityJob : IJobParallelFor
        {
            [ReadOnly] public NativeFilter Entities;
            public NativeStash<Position> PositionComponents;
            public NativeStash<Velocity> VelocityComponents;
            public float Delta;

            public void Execute(int index)
            {
                var entityId = Entities[index];

                ref var position = ref PositionComponents.Get(entityId, out var requestExists);
                ref var velocity = ref VelocityComponents.Get(entityId, out var velocityExists);
                if (velocityExists && requestExists)
                {
                    position.Value += velocity.Value * Delta;
                }
            }
        }
    }
}