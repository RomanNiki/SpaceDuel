using Core.Characteristics.EnergyLimits.Components;
using Core.Input.Components;
using Core.Movement.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Movement.Systems
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
        private Stash<Speed> _moveForcePool;
        private Stash<ForceRequest> _forceRequestPool;

        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<InputMoveData>().With<Velocity>().With<Rotation>().With<Mass>()
                .Without<NoEnergyBlock>().Build();

            _inputMoveDataPool = World.GetStash<InputMoveData>();
            _rotationPool = World.GetStash<Rotation>();
            _massPool = World.GetStash<Mass>();
            _moveForcePool = World.GetStash<Speed>();
            _forceRequestPool = World.GetStash<ForceRequest>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                if (_inputMoveDataPool.Get(entity).Accelerate == false) continue;

                ref var rotation = ref _rotationPool.Get(entity);
                ref var mass = ref _massPool.Get(entity);
                ref var moveForce = ref _moveForcePool.Get(entity);
                _forceRequestPool.Add(entity);
                _forceRequestPool.Get(entity).Value += (Vector2)rotation.LookDir * (moveForce.Value / mass.Value);
            }
        }
        
        public void Dispose()
        {
        }
    }
}