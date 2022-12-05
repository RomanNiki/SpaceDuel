using Leopotam.Ecs;
using Model.Components.Requests;

namespace Model.Systems
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