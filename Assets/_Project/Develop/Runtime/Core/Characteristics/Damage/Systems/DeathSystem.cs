using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Extensions;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class DeathSystem : ISystem
    {
        private Filter _filter;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<DeadTag>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                World.SendMessage(new DestroyRequest() { EntityToDestroy = entity });
                World.SendMessage(new DestroyEvent() { EntityToDestroy = entity });
            }
        }

        public void Dispose()
        {
        }
    }
}