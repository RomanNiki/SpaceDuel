using _Project.Develop.Runtime.Engine.ECS.UI.PlayerUI.Systems;
using _Project.Develop.Runtime.Engine.ECS.UI.PlayerUI.Weapon.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.ECS.UI.Statistics
{
    public class UIStatisticFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new EnergyUISystem());
            AddSystem(new HealthUISystem());
            AddSystem(new WeaponTimerSliderSystem());
        }
    }
}