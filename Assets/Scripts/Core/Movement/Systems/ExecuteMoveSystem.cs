﻿using Core.Movement.Components;
using Core.Views.Components;
using Scellecs.Morpeh;

namespace Core.Movement.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class ExecuteMoveSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<Rotation> _rotationPool;
        private Stash<Position> _positionPool;
        private Stash<ViewObject> _viewPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<Position>().With<Rotation>().With<ViewObject>().Build();
            _rotationPool = World.GetStash<Rotation>();
            _positionPool = World.GetStash<Position>();
            _viewPool = World.GetStash<ViewObject>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var position = ref _positionPool.Get(entity);
                ref var rotation = ref _rotationPool.Get(entity);
                ref var view = ref _viewPool.Get(entity);
                
                view.Value.MoveTo(position.Value);
                view.Value.RotateTo(rotation.Value);
            }
        }

        public void Dispose()
        {
        }
    }
}