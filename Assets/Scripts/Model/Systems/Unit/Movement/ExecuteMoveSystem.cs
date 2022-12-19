using Leopotam.Ecs;
using Model.Components.Unit.MoveComponents;

namespace Model.Systems.Unit.Movement
{
    public class ExecuteMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Position, Rotation, ViewObjectComponent> _moveFilter = null;
        
        public void Run()
        {
            foreach (var i in _moveFilter)
            {
                ref var position = ref _moveFilter.Get1(i);
                ref var rotation = ref _moveFilter.Get2(i);
                ref var view = ref _moveFilter.Get3(i).ViewObject;
                
                view.MoveTo(position.Value);
                view.Rotation = rotation.Value;
            }
        }
    }
}