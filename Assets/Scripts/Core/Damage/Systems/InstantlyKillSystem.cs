using Core.Damage.Components;
using Core.Player.Components;
using Scellecs.Morpeh;

namespace Core.Damage.Systems
{
    public sealed class InstantlyKillSystem : ISystem
    {
        private Filter _killFilter;
        private Stash<Health> _healthPool;
        private Stash<KillRequest> _killRequestPool;
        private Stash<DamageRequest> _damageRequestPool;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            _killFilter = World.Filter.With<KillRequest>().With<Health>().Without<DeadTag>();
            _healthPool = World.GetStash<Health>();
            _killRequestPool = World.GetStash<KillRequest>();
            _damageRequestPool = World.GetStash<DamageRequest>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _killFilter)
            {
                if (_damageRequestPool.Has(entity) == false)
                {
                    _damageRequestPool.Add(entity);
                }

                _damageRequestPool.Get(entity).Value = _healthPool.Get(entity).Value;
                
                _killRequestPool.Remove(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}