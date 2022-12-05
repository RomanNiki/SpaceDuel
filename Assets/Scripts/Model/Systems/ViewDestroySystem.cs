using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents;

namespace Model.Systems
{
    public class ViewDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewObjectComponent, EntityDestroyRequest> _filterWithView = null;
        
        public void Run()
        {
            foreach (var i in _filterWithView)
            {
                _filterWithView.Get1(i).ViewObject.Destroy();
            }
        }
    }
}