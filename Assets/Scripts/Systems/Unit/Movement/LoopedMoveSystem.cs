using Components.Unit.MoveComponents;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Systems.Unit.Movement
{
    internal sealed class LoopedMoveSystem : IEcsRunSystem
    {
        [Inject] private Camera _camera;
        private readonly EcsFilter<Move> _player = null;
        
        public void Run()
        {
            foreach (var i in _player)
            {
                ref var moveComponent = ref _player.Get1(i);
                var nextPosition = _camera.WorldToViewportPoint(moveComponent.Position) ;
                nextPosition.y = Mathf.Repeat(nextPosition.y, 1);
                nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
                var point = _camera.ViewportToWorldPoint(new Vector3(nextPosition.x, nextPosition.y, 0));
                moveComponent.Position = point;
            }
        }
    }
}