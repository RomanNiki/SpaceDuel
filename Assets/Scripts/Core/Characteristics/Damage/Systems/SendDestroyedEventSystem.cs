using Core.Characteristics.Damage.Components;
using Core.Extensions;
using Core.Movement.Components;

namespace Core.Characteristics.Damage.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class SendDestroyedEventSystem<TTag> : ISystem
        where TTag : struct, IComponent
    {
        private Filter _filter;
        private Stash<Position> _positionPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<TTag>().With<Position>().With<DestroySelfRequest>().Build();
            _positionPool = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var position = ref _positionPool.Get(entity);
                World.SendMessage(new DestroyedEvent<TTag> { Position = position.Value });
            }
        }

        public void Dispose()
        {
        }
    }
}