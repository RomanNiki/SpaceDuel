using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents;

namespace Model.Systems
{
    public class EntityDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewObjectComponent, EntityDestroyRequest> _filterWithView = null;
        private readonly EcsFilter<EntityDestroyRequest> _filterWithoutView = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterWithView)
            {
                _filterWithView.Get1(i).ViewObject.Destroy();
            }

            foreach (var i in _filterWithoutView)
            {
                _filterWithoutView.GetEntity(i).Destroy();
            }
        }
    }
}