using Leopotam.Ecs;
using Model.Unit.Destroy.Components.Requests;
using Model.Weapons.Components;

namespace Model
{
    public sealed class OwnerIsAliveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerOwner> _playerOwnerFilter = null;
        
        public void Run()
        {
            foreach (var i in _playerOwnerFilter)
            {
                ref var owner = ref _playerOwnerFilter.Get1(i).Owner;
                if (owner.IsAlive() == false)
                {
                    _playerOwnerFilter.GetEntity(i).Get<EntityDestroyRequest>();
                }
            }
        }
    }
}