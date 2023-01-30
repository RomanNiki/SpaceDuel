using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Destroy.Components.Requests;

namespace Model.Unit.Destroy
{
    public sealed class ViewDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewObjectComponent, EntityDestroyRequest> _destroyViewFilter = null;
        
        public void Run()
        {
            foreach (var i in _destroyViewFilter)
            {
                _destroyViewFilter.Get1(i).ViewObject.Destroy();
            }
        }
    }
}