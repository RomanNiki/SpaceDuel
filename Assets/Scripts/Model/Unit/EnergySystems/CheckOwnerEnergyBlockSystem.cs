using Leopotam.Ecs;
using Model.Unit.EnergySystems.Components;
using Model.Weapons.Components;

namespace Model.Unit.EnergySystems
{
    public sealed class CheckOwnerEnergyBlockSystem<TTag> : IEcsRunSystem
    where TTag : struct
    {
        private readonly EcsFilter<PlayerOwner, TTag> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var owner = ref _filter.Get1(i).Owner;
                if (owner.IsAlive() == false) continue;
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