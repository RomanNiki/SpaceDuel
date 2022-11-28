using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Components.Weapons;

namespace Model.Systems.Weapons
{
    public class WeaponEnergyDischargeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerOwner, DischargeShotContainer, ShotMadeEvent> _weapon = null;

        public void Run()
        {
            foreach (var j in _weapon)
            {
                ref var owner = ref _weapon.Get1(j);
                owner.Owner.Get<DischargeRequest>().Value += _weapon.Get2(j).DischargeRequest.Value;
            }
        }
    }
}