using Leopotam.Ecs;
using Model.Unit.Destroy.Components.Requests;
using Model.Weapons.Components;

namespace Model
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