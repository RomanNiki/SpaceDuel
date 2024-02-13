using System;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
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

    public sealed class RotateSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<Rotation> _rotationPool;
        private Stash<RotationSpeed> _rotationSpeedPool;
        private Stash<InputMoveData> _inputDataPool;
        private Stash<Energy> _energyPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Rotation>().With<RotationSpeed>().With<InputMoveData>().With<Energy>()
                .Build();
            _rotationPool = World.GetStash<Rotation>();
            _inputDataPool = World.GetStash<InputMoveData>();
            _rotationSpeedPool = World.GetStash<RotationSpeed>();
            _energyPool = World.GetStash<Energy>();
        }

        public void OnUpdate(float deltaTime)
        {
#if UNITY_WEBGL == false
            var filter = _filter.AsNative();
            var job = new RotateJob()
            {
                Delta = deltaTime,
                Entities = filter,
                RotationSpeedComponents = _rotationSpeedPool.AsNative(),
                InputComponents = _inputDataPool.AsNative(),
                RotationComponents = _rotationPool.AsNative()
            };

            World.JobHandle = job.Schedule(filter.length, 64, World.JobHandle);
            World.JobsComplete();
#else
            foreach (var entity in _filter)
            {
                if (_energyPool.Get(entity).HasEnergy == false)
                    continue;
                
                ref var inputData = ref _inputDataPool.Get(entity);
                if (MathF.Abs(inputData.Rotation) < 0.1f)
                    continue;

                ref var rotation = ref _rotationPool.Get(entity);
                ref var rotationSpeed = ref _rotationSpeedPool.Get(entity);
          
                Rotate(ref rotation, inputData.Rotation * (rotationSpeed.Value * deltaTime));
            }
#endif
        }

        public void Dispose()
        {
        }

        private static void Rotate(ref Rotation rotation, float delta) =>
            rotation.Value = Mathf.Repeat(rotation.Value + delta, 360f);

#if UNITY_WEBGL == false

        [BurstCompile]
        private struct RotateJob : IJobParallelFor
        {
            [ReadOnly] public NativeFilter Entities;
            public NativeStash<Rotation> RotationComponents;
            public NativeStash<InputMoveData> InputComponents;
            public NativeStash<RotationSpeed> RotationSpeedComponents;
            public NativeStash<Energy> EnergyComponents;
            public float Delta;

            public void Execute(int index)
            {
                var entityId = Entities[index];
                if (EnergyComponents.Get(entity).HasEnergy == false)
                    return;
                ref var inputData = ref InputComponents.Get(entityId);
                if (MathF.Abs(inputData.Rotation) < 0.1f)
                    return;

                ref var rotation = ref RotationComponents.Get(entityId);
                ref var rotationSpeed = ref RotationSpeedComponents.Get(entityId);

                Rotate(ref rotation, inputData.Rotation * (rotationSpeed.Value * Delta));
            }
        }
#endif
    }
}