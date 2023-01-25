using Leopotam.Ecs;
using Model.Unit.Destroy.Components.Requests;

namespace Model.Unit.Destroy
{
    public sealed class EntityDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<EntityDestroyRequest> _filterWithoutView = null;

        public void Run()
        {
            foreach (var i in _filterWithoutView)
            {
                _filterWithoutView.GetEntity(i).Destroy();
            }
        }
    }
}