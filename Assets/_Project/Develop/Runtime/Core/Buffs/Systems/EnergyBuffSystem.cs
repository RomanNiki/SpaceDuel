using _Project.Develop.Runtime.Core.Buffs.Components;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Extensions;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Buffs.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class EnergyBuffSystem : ISystem
    {
        private Filter _filter;
        private Stash<ChargeContainer> _chargeContainerStash;
        private Stash<BuffRequest> _buffRequestStash;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<BuffRequest>().Build();
            _chargeContainerStash = World.GetStash<ChargeContainer>();
            _buffRequestStash = World.GetStash<BuffRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var buffRequest = ref _buffRequestStash.Get(entity);
                if (buffRequest.Buff.IsNullOrDisposed() || buffRequest.Player.IsNullOrDisposed())
                {
                    continue;
                }

                if (_chargeContainerStash.Has(buffRequest.Buff) == false) continue;
                ref var chargeContainer = ref _chargeContainerStash.Get(buffRequest.Buff);
                World.SendMessage(
                    new ChargeRequest() { Entity = buffRequest.Player, Value = chargeContainer.Value });
            }
        }

        public void Dispose()
        {
        }
    }
}