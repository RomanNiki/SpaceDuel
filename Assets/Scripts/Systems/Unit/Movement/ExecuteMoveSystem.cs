using Components.Unit.MoveComponents;
using Leopotam.Ecs;

namespace Systems.Unit.Movement
{
    public class ExecuteMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Move, View> _moveFilter = null;
        
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