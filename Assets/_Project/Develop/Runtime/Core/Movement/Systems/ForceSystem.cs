﻿using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Input.Components;
using _Project.Develop.Runtime.Core.Movement.Components;
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

    public sealed class ForceSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<InputMoveData> _inputMoveDataPool;
        private Stash<Rotation> _rotationPool;
        private Stash<Mass> _massPool;
        private Stash<Speed> _speedPool;
        private Stash<Energy> _energyPool;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<InputMoveData>().With<Velocity>().With<Rotation>().With<Mass>()
                .With<Energy>().Build();

            _inputMoveDataPool = World.GetStash<InputMoveData>();
            _rotationPool = World.GetStash<Rotation>();
            _massPool = World.GetStash<Mass>();
            _speedPool = World.GetStash<Speed>();
            _energyPool = World.GetStash<Energy>();
        }

        public void OnUpdate(float deltaTime)
        {
#if UNITY_WEBGL == false
            var filter = _filter.AsNative();
            var forceRequestOutput = new NativeArray<ForceRequest>(filter.length, Allocator.TempJob);
            var job = new ForceJobReference()
            {
                Entities = filter,
                SpeedPool = _speedPool.AsNative(),
                ForceRequests = forceRequestOutput,
                MassPool = _massPool.AsNative(),
                RotationPool = _rotationPool.AsNative(),
                InputMoveDataPool = _inputMoveDataPool.AsNative(),
                EnergyPool = _energyPool.AsNative(),
            };
            World.JobHandle = job.Schedule(filter.length, 64, World.JobHandle);
            World.JobsComplete();
            foreach (var force in forceRequestOutput)
            {
                World.SendMessage(force);
            }

            forceRequestOutput.Dispose();
#else
            foreach (var entity in _filter)
            {
                if (_energyPool.Get(entity).HasEnergy == false) continue;
                if (_inputMoveDataPool.Get(entity).Accelerate == false) continue;

                ref var rotation = ref _rotationPool.Get(entity);
                ref var mass = ref _massPool.Get(entity);
                ref var moveForce = ref _speedPool.Get(entity);
                var force = (Vector2)rotation.LookDir * (moveForce.Value / mass.Value);

                World.SendMessage(new ForceRequest { Value = force, Entity = entity });
            }
#endif
        }

        public void Dispose()
        {
        }

#if UNITY_WEBGL == false
        [BurstCompile]
        private struct ForceJobReference : IJobParallelFor
        {
            [ReadOnly] public NativeFilter Entities;
            public NativeStash<InputMoveData> InputMoveDataPool;
            public NativeStash<Rotation> RotationPool;
            public NativeStash<Mass> MassPool;
            public NativeStash<Speed> SpeedPool;
            public NativeStash<Energy> EnergyPool;
            public NativeArray<ForceRequest> ForceRequests;

            public void Execute(int index)
            {
                var entity = Entities[index];
                if (EnergyPool.Get(entity).HasEnergy == false) return;
                if (InputMoveDataPool.Get(entity).Accelerate == false) return;

                ref var rotation = ref RotationPool.Get(entity);
                ref var mass = ref MassPool.Get(entity);
                ref var moveForce = ref SpeedPool.Get(entity);
                var force = (Vector2)rotation.LookDir * (moveForce.Value / mass.Value);
                ForceRequests[index] = new ForceRequest() { Value = force, EntityId = entity };
            }
        }

#endif
    }
}