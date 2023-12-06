using _Project.Develop.Runtime.Core.Buffs.Components;
using _Project.Develop.Runtime.Core.Buffs.Systems;
using _Project.Develop.Runtime.Core.Services.Random;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Buffs
{
    public class BuffFeature : UpdateFeature
    {
        private readonly IRandom _random;

        public BuffFeature(IRandom random)
        {
            _random = random;
        }
        
        protected override void Initialize()
        {
            AddSystem(new EnergyBuffSystem());
            AddSystem(new SpawnBuffSystem(_random));
            RegisterRequest<BuffRequest>();
        }
    }
}