using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Views.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Effects.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public abstract class CreateDestroyEffectSystem<TDestroyedTag, TEffectTag> : ISystem
        where TDestroyedTag : struct, IComponent
        where TEffectTag : struct, IComponent
    {
        private Filter _destroyEventFilter;
        private Stash<Position> _positionPool;
        private Stash<TEffectTag> _effectTagPool;
        private Stash<TDestroyedTag> _destroyedTagPool;
        private Stash<DestroyEvent> _destroyEventPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _destroyEventFilter = World.Filter.With<DestroyEvent>().Build();
            _destroyEventPool = World.GetStash<DestroyEvent>();
            _positionPool = World.GetStash<Position>();
            _effectTagPool = World.GetStash<TEffectTag>();
            _destroyedTagPool = World.GetStash<TDestroyedTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var requestEntity in _destroyEventFilter)
            {
                ref var entityToDestroy = ref _destroyEventPool.Get(requestEntity).EntityToDestroy;
                if (entityToDestroy.IsNullOrDisposed())
                    continue;
                if (_destroyedTagPool.Has(entityToDestroy) == false)
                    continue;
                
                ref var explosionPosition = ref _positionPool.Get(entityToDestroy);
                CreateEffectEntity(explosionPosition.Value);
            }
        }

        private void CreateEffectEntity(Vector2 position)
        {
            var entity = World.CreateEntity();
            _effectTagPool.Add(entity);
            World.SendMessage(new SpawnRequest(entity, GetObjectId(), position, 0f));
        }

        protected abstract ObjectId GetObjectId();

        public void Dispose()
        {
        }
    }
}