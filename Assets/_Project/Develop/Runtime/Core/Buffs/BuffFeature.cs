using _Project.Develop.Runtime.Core.Buffs.Components;
using _Project.Develop.Runtime.Core.Buffs.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Buffs
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