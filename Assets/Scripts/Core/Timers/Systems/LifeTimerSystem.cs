using Core.Damage.Components;
using Core.Player.Components;
using Core.Timers.Components;
using Scellecs.Morpeh;

namespace Core.Timers.Systems
{
    public class LifeTimerSystem : ISystem
    {
        private Filter _filter;
        private Stash<KillRequest> _killRequestPool;
        private Stash<DestroyRequest> _destroyRequestPool;
        private Stash<Health> _healthPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<DieWithoutLifeTimerTag>().Without<Timer<LifeTimer>>();
            _killRequestPool = World.GetStash<KillRequest>();
            _destroyRequestPool = World.GetStash<DestroyRequest>();
            _healthPool = World.GetStash<Health>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                if (_healthPool.Has(entity))
                {
                    _killRequestPool.Add(entity);
                    continue;
                }

                _destroyRequestPool.Add(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}