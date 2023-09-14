using Core.Extensions;
using Core.Movement.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh.Native;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Core.Movement.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class ProjectileLookAtVelocitySystem : ISystem
    {
        private Filter _filter;
        private Stash<Velocity> _velocityPool;
        private Stash<Rotation> _rotationPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Velocity>().With<Rotation>().With<ProjectileTag>().Build();
            _velocityPool = World.GetStash<Velocity>();
            _rotationPool = World.GetStash<Rotation>();
        }

        public void OnUpdate(float deltaTime)
        {
            var filter = _filter.AsNative();
            var job = new LookAtVelocityJob()
            {
                Entities = filter,
                VelocityComponents = _velocityPool.AsNative(),
                RotationComponents = _rotationPool.AsNative()
            };

            var handler = job.Schedule(filter.length, 64);
            handler.Complete();
        }

        public void Dispose()
        {
        }

        [BurstCompile]
        private struct LookAtVelocityJob : IJobParallelFor
        {
            [ReadOnly] public NativeFilter Entities;
            public NativeStash<Velocity> VelocityComponents;
            public NativeStash<Rotation> RotationComponents;

            public void Execute(int index)
            {
                var entityId = Entities[index];
                ref var velocity = ref VelocityComponents.Get(entityId);
                ref var rotation = ref RotationComponents.Get(entityId);
                if (velocity.Value.magnitude <= 0.1f)
                {
                    return;
                }
                rotation.Value = MathExtensions.CalculateRotationFromVelocity(velocity.Value);
            }
        }
    }
}