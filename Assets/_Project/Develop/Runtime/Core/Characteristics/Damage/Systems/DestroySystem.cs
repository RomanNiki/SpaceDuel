using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Views.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.EntityPool;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class DestroySystem : ISystem
    {
        private Filter _destroyRequestFilter;
        private Stash<ViewObject> _viewPool;
        private Stash<DestroyRequest> _destroyRequestPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _destroyRequestFilter = World.Filter.With<DestroyRequest>().Build();
            _destroyRequestPool = World.GetStash<DestroyRequest>();
            _viewPool = World.GetStash<ViewObject>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var requestEntity in _destroyRequestFilter)
            {
                ref var entityToDestroy = ref _destroyRequestPool.Get(requestEntity).EntityToDestroy;
                if (entityToDestroy.IsNullOrDisposed())
                    continue;

                if (_viewPool.Has(entityToDestroy))
                {
                    _viewPool.Get(entityToDestroy).Value.Dispose();
                }
                else
                {
                    World.RemoveEntity(entityToDestroy);
                }
                World.PoolEntity(requestEntity);
            }
        }

        public void Dispose()
        {
        }
    }
}