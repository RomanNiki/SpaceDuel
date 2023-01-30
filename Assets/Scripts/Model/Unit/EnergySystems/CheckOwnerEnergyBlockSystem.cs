using Leopotam.Ecs;
using Model.Unit.EnergySystems.Components;
using Model.Weapons.Components;

namespace Model.Unit.EnergySystems
{
    public sealed class CheckOwnerEnergyBlockSystem<TTag> : IEcsRunSystem
    where TTag : struct
    {
        private readonly EcsFilter<PlayerOwner, TTag> _ownerEnergyFilter = null;
        
        public void Run()
        {
            foreach (var i in _ownerEnergyFilter)
            {
                ref var owner = ref _ownerEnergyFilter.Get1(i).Owner;
                if (owner.IsAlive() == false) continue;
                ref var gun = ref _ownerEnergyFilter.GetEntity(i);

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