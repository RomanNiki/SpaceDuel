using Core.EnergyLimits.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;

namespace Core.Weapon.Systems
{
    public class WeaponEnergyDischargeSystem : ISystem
    {
        private Filter _filter;
        private Stash<DischargeRequest> _dischargeRequestPool;
        private Stash<DischargeContainer> _dischargeContainerPool;
        private Stash<PlayerOwner> _ownerPool;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<PlayerOwner>().With<DischargeContainer>().With<ShotMadeEvent>();
            _dischargeRequestPool = World.GetStash<DischargeRequest>();
            _dischargeContainerPool = World.GetStash<DischargeContainer>();
            _ownerPool = World.GetStash<PlayerOwner>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity).Entity;
                
                if (owner == null) continue;
                
                if (_dischargeRequestPool.Has(owner) == false)
                {
                    _dischargeRequestPool.Add(owner);
                }

                _dischargeRequestPool.Get(owner).Value +=
                    _dischargeContainerPool.Get(entity).DischargeRequest.Value;
            }
        }

        public void Dispose()
        {
        }
    }
}