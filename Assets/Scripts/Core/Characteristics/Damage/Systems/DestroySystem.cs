﻿using Core.Characteristics.Damage.Components;
using Core.Views.Components;
using Scellecs.Morpeh;

namespace Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class DestroySystem : ISystem
    {
        private Filter _viewDestroyFilter;
        private Filter _destroyEntityFilter;
        private Stash<ViewObject> _viewPool;

        public World World { get; set; }

        public void OnAwake()
        {
            _viewDestroyFilter = World.Filter.With<DestroySelfRequest>().With<ViewObject>().Build();
            _destroyEntityFilter = World.Filter.With<DestroySelfRequest>().Without<ViewObject>().Build();
            _viewPool = World.GetStash<ViewObject>().AsDisposable();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _viewDestroyFilter)
            {
                _viewPool.Remove(entity);
            }

            foreach (var entity in _destroyEntityFilter)
            {
                World.RemoveEntity(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}