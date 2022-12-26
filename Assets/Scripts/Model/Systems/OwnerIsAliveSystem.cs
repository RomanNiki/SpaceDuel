using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Weapons;

namespace Model.Systems
{
    public sealed class OwnerIsAliveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerOwner> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var owner = ref _filter.Get1(i).Owner;
                if (owner.IsAlive() == false)
                {
                    _filter.GetEntity(i).Get<EntityDestroyRequest>();
                }
            }
        }
    }
}