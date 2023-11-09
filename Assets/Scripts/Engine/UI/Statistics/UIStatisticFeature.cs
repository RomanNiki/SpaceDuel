using Engine.UI.Statistics.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Engine.UI.Statistics
{
    public class UIStatisticFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new EnergyUISystem());
            AddSystem(new HealthUISystem());
        }
    }
}