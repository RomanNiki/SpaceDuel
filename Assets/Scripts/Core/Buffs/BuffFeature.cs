using Core.Buffs.Components;
using Core.Buffs.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Buffs
{
    public class BuffFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new EnergyBuffSystem());
            RegisterRequest<BuffRequest>();
        }
    }
}