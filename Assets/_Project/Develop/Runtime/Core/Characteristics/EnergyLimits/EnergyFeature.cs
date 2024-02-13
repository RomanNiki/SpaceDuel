using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits
{
    public class EnergyFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            RegisterEvent<EnergyChangedEvent>();
            RegisterRequest<ChargeRequest>();
            RegisterRequest<DischargeRequest>();
            AddSystem(new NoEnergyGravityResistSystem());
            AddSystem(new MoveDischargeSystem());
            AddSystem(new WeaponEnergyDischargeSystem());
            AddSystem(new SunDischargeSystem());
            AddSystem(new SunChargeSystem());
            AddSystem(new DischargeSystem());
            AddSystem(new ChargeSystem());
            AddSystem(new EnergyBlockSystem());
        }
    }
}