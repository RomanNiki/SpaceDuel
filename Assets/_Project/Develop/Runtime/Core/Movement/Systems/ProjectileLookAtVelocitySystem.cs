﻿using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh;
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

    public sealed class ProjectileLookAtVelocitySystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<Velocity> _velocityPool;
        private Stash<Rotation> _rotationPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Velocity>().With<Rotation>().With<BulletTag>().Build();
            _velocityPool = World.GetStash<Velocity>();
            _rotationPool = World.GetStash<Rotation>();
        }

        public void OnUpdate(float deltaTime)
        {
#if UNITY_WEBGL == false
            var filter = _filter.AsNative();
            var job = new LookAtVelocityJob()
            {
                Entities = filter,
                VelocityComponents = _velocityPool.AsNative(),
                RotationComponents = _rotationPool.AsNative()
            };

            World.JobHandle = job.Schedule(filter.length, 64, World.JobHandle);
            World.JobsComplete();
#else
            foreach (var entity in _filter)
            {
                ref var velocity = ref _velocityPool.Get(entity);
                ref var rotation = ref _rotationPool.Get(entity);
                if (velocity.Value.magnitude <= 0.1f)
                {
                    return;
                }

                rotation.Value = MathExtensions.CalculateRotationFromVelocity(velocity.Value);
            }
#endif
        }

        public void Dispose()
        {
        }

#if UNITY_WEBGL == false

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
#endif
    }
}