using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Engine.Views.Effects.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.Views.Effects
{
    public class ViewEffectFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new VisualizeLifeTimerByMaterialSystem<InvisibleTimer>());
            AddSystem(new VisualizeLifeTimerByMaterialSystem<LifeTimer>());
            AddSystem(new VisualizeEnergyByMaterialSystem());
        }
    }
}