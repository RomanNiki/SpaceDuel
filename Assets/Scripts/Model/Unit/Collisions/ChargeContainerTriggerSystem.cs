using Leopotam.Ecs;
using Model.Buffs.Components.Tags;
using Model.Extensions;
using Model.Unit.Collisions.Components.Events;
using Model.Unit.Destroy.Components.Requests;
using Model.Unit.EnergySystems.Components;
using Model.Unit.EnergySystems.Components.Requests;

namespace Model.Unit.Collisions
{
    public sealed class ChargeContainerTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, ChargeContainer, BuffTag> _chargersFilter =
            null;

        public void Run()
        {
            foreach (var i in _chargersFilter)
            {
               ref var collisions = ref _chargersFilter.Get1(i);
               ref var charge = ref _chargersFilter.Get2(i);
               ref var entity = ref _chargersFilter.GetEntity(i);
               foreach (var collision in collisions.List)
               {
                   collision.Other.Get<ChargeRequest>().Value += charge.ChargeRequest.Value;
               }
               entity.Get<EntityDestroyRequest>();
            }
        }
    }
}