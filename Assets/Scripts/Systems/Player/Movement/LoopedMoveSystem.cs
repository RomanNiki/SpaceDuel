using Components.Player;
using Components.Player.MoveComponents;
using Leopotam.Ecs;
using Tags;
using UnityEngine;
using Zenject;

namespace Systems.Player.Movement
{
    public sealed class LoopedMoveSystem : IEcsRunSystem
    {
        [Inject] private Camera _camera;
        private readonly EcsFilter<PlayerTag, Move, Energy> _player = null;
        
        public void Run()
        {
            foreach (var i in _player)
            {
                ref var moveComponent = ref _player.Get2(i);
                var nextPosition = _camera.WorldToViewportPoint(moveComponent.ViewObject.Position) ;
                nextPosition.y = Mathf.Repeat(nextPosition.y, 1);
                nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
                var point = _camera.ViewportToWorldPoint(new Vector3(nextPosition.x, nextPosition.y, 0));
                moveComponent.ViewObject.Position = point;
            }
        }
    }
}