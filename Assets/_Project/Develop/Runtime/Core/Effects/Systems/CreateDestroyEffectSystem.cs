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
        private Filter _filter;
        private Stash<Position> _positionPool;
        private Stash<TEffectTag> _effectTagPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<TDestroyedTag>().With<DestroySelfRequest>().Build();
            _positionPool = World.GetStash<Position>();
            _effectTagPool = World.GetStash<TEffectTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var explosionPosition = ref _positionPool.Get(entity);
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