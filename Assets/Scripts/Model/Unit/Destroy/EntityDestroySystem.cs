using Leopotam.Ecs;
using Model.Unit.Destroy.Components.Requests;

namespace Model.Unit.Destroy
{
    public sealed class EntityDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<EntityDestroyRequest> _destroyEntityFilter = null;

        public void Run()
        {
            foreach (var i in _destroyEntityFilter)
            {
                _destroyEntityFilter.GetEntity(i).Destroy();
            }
        }
    }
}