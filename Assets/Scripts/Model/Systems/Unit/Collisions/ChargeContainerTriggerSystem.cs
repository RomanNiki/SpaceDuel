using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Extensions;
using Model.Components.Requests;
using Model.Components.Unit;

namespace Model.Systems.Unit.Collisions
{
    public class ChargeContainerTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, ChargeContainer> _filter =
            null;

        public void Run()
        {
            foreach (var i in _filter)
            {
               ref var collisions = ref _filter.Get1(i);
               ref var charge = ref _filter.Get2(i);
               ref var entity = ref _filter.GetEntity(i);
               foreach (var collision in collisions.List)
               {
                   collision.Other.Get<ChargeRequest>().Value += charge.ChargeRequest.Value;
               }
               entity.Get<InstantlyKill>();
            }
        }
    }
}