using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class InstantlyKillSystem : ISystem
    {
        private Filter _killFilter;
        private Stash<Health> _healthPool;
        private Stash<KillSelfRequest> _killSelfRequestPool;
        private Stash<Position> _positionPool;

        public World World { get; set; }
        
        public void OnAwake()
        {
            _killFilter = World.Filter.With<KillSelfRequest>().With<Health>().Without<DeadTag>().Build();
            _healthPool = World.GetStash<Health>();
            _killSelfRequestPool = World.GetStash<KillSelfRequest>();
            _positionPool = World.GetStash<Position>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _killFilter)
            {
                World.SendMessage(new DamageRequest(_healthPool.Get(entity).Value, _positionPool.Get(entity).Value, entity));
                _killSelfRequestPool.Remove(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}