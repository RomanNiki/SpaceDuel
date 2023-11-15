using _Project.Develop.Runtime.Engine.UI.Statistics.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.UI.Statistics
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