using Core.Characteristics.Damage.Components;
using Core.Common.Enums;
using Core.Extensions;
using Core.Movement.Components;
using Core.Views.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Effects.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class DestroyEffectCreateSystem<TDestroyedTag, TEffectTag> : ISystem
        where TDestroyedTag : struct, IComponent
        where TEffectTag : struct, IComponent
    {
        private Filter _filter;
        private Stash<DestroyedEvent<TDestroyedTag>> _destroyEventPool;
        private Stash<TEffectTag> _effectTagPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<DestroyedEvent<TDestroyedTag>>().Build();
            _destroyEventPool = World.GetStash<DestroyedEvent<TDestroyedTag>>();
            _effectTagPool = World.GetStash<TEffectTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var explosionPosition = ref _destroyEventPool.Get(entity);
                CreateExplosionEntity(explosionPosition.Position);
            }
        }

        private void CreateExplosionEntity(Vector2 position)
        {
            var entity = World.CreateEntity();
            _effectTagPool.Add(entity);
            World.SendMessage(new ViewCreateRequest(entity, position, 0f));
        }

        public void Dispose()
        {
        }
    }
}