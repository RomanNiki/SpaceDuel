using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Destroy.Components.Requests;

namespace Model.Unit.Destroy
{
    public sealed class ViewDestroySystem : IEcsRunSystem
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