using Leopotam.Ecs;
using Model.Unit.EnergySystems.Components.Requests;
using Model.Weapons.Components;
using Model.Weapons.Components.Events;

namespace Model.Weapons
{
    public sealed class WeaponEnergyDischargeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerOwner, DischargeShotContainer, ShotMadeEvent> _weaponFilter = null;

        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                ref var owner = ref _weaponFilter.Get1(i);
                owner.Owner.Get<DischargeRequest>().Value += _weaponFilter.Get2(i).DischargeRequest.Value;
            }
        }
    }
}