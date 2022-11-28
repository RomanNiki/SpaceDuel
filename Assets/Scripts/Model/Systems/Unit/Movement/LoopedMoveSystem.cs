using Leopotam.Ecs;
using Model.Components.Unit.MoveComponents;
using UnityEngine;
using Zenject;

namespace Model.Systems.Unit.Movement
{
    public sealed class LoopedMoveSystem : IEcsRunSystem
    {
        [Inject] private Camera _camera;
        private readonly EcsFilter<TransformData> _transformFilter = null;
        
        public void Run()
        {
            foreach (var i in _transformFilter)
            {
                ref var moveComponent = ref _transformFilter.Get1(i);
                var nextPosition = _camera.WorldToViewportPoint(moveComponent.Position) ;
                nextPosition.y = Mathf.Repeat(nextPosition.y, 1);
                nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
                var point = _camera.ViewportToWorldPoint(new Vector3(nextPosition.x, nextPosition.y, 0));
                moveComponent.Position = point;
            }
        }
    }
}