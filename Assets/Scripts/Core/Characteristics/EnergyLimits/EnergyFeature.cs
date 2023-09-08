using Core.Characteristics.EnergyLimits.Components;
using Core.Characteristics.EnergyLimits.Systems;
using Core.Extensions;
using Core.Extensions.Clear.Systems;
using Cysharp.Threading.Tasks;

namespace Core.Characteristics.EnergyLimits
{
    public class EnergyFeature : BaseMorpehFeature
    {
        protected async override UniTask InitializeSystems()
        {
            AddSystem(new DellHereFixedUpdateSystem<EnergyChangedEvent>());
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