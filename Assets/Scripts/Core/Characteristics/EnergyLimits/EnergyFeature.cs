using Core.Characteristics.EnergyLimits.Components;
using Core.Characteristics.EnergyLimits.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Characteristics.EnergyLimits
{
    public class EnergyFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            RegisterEvent<EnergyChangedEvent>();
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