using Leopotam.Ecs;
using Model.Components;
using Model.Components.Weapons;

namespace Model.Systems.Weapons
{
    public class CheckOwnerEnergySystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerOwner, WeaponType> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var owner = ref _filter.Get1(i).Owner;
                ref var gun = ref _filter.GetEntity(i);
                if (owner.Has<NoEnergyBlock>())
                {
                    gun.Get<NoEnergyBlock>();
                }
                else
                {
                    if (gun.Has<NoEnergyBlock>())
                    {
                        gun.Del<NoEnergyBlock>();
                    }
                }
            }
        }
    }
}