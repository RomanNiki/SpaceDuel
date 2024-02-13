using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public class WeaponEnergyDischargeSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<DischargeContainer> _dischargeContainerPool;
        private Stash<Owner> _ownerPool;
        private Stash<ShotMadeEvent> _shotMadeEventPool;

        public override void OnAwake()
        {
            _filter = World.Filter.With<ShotMadeEvent>().Build();
            _shotMadeEventPool = World.GetStash<ShotMadeEvent>();
            _dischargeContainerPool = World.GetStash<DischargeContainer>();
            _ownerPool = World.GetStash<Owner>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var weaponEntity = ref _shotMadeEventPool.Get(entity).Weapon;
                if (weaponEntity.IsNullOrDisposed()) continue;

                ref var owner = ref _ownerPool.Get(weaponEntity).Entity;

                if (owner.IsNullOrDisposed()) continue;
                World.SendMessage(new DischargeRequest
                    { Entity = owner, Value = _dischargeContainerPool.Get(weaponEntity).Value });
            }
        }
    }
}