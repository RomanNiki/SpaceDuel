using Leopotam.Ecs;
using Model.Components.Unit.MoveComponents;

namespace Model.Systems.Unit.Movement
{
    public class ExecuteMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformData, ViewObjectComponent> _moveFilter = null;
        
        public void Run()
        {
            foreach (var i in _moveFilter)
            {
                ref var move = ref _moveFilter.Get1(i);
                ref var view = ref _moveFilter.Get2(i).ViewObject;
                
                view.MoveTo(move.Position);
                view.Rotation = move.Rotation;
            }
        }
    }
}