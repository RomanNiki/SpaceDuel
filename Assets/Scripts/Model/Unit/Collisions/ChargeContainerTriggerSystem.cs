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
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, ChargeContainer, BuffTag> _chargersFilter;

        public void Run()
        {
            foreach (var i in _chargersFilter)
            {
                var collisions = _chargersFilter.Get1(i).List;
                ref var charge = ref _chargersFilter.Get2(i);
                ref var entity = ref _chargersFilter.GetEntity(i);
                for (var j = 0; j < collisions.Count; j++)
                {
                    var collision = collisions.Dequeue();
                    collision.Other.Get<ChargeRequest>().Value += charge.ChargeRequest.Value;
                }

                entity.Get<EntityDestroyRequest>();
            }
        }
    }
}