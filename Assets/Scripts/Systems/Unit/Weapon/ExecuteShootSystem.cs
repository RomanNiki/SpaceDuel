using Components.Unit.Weapon;
using Leopotam.Ecs;

namespace Systems.Unit.Weapon
{
    public class ExecuteShootSystem : IEcsRunSystem
    { 
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<BulletFactory, Shooting, PlayerOwner, BulletStartForce> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
              ref var factory = ref _filter.Get1(i).Factory;
              var bullet = factory.Create();
            }
        }
    }
}