using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Timers.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Timers.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class LifeCycleSystem : ISystem
    {
        private Filter _filter;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<DieWithoutLifeTimerTag>().Without<Timer<LifeTimer>>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                World.SendMessage(new KillRequest() { EntityToKill = entity });
            }
        }

        public void Dispose()
        {
        }
    }
}