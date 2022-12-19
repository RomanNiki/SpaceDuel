using Leopotam.Ecs;
using Model.Components.Unit.MoveComponents;
using Model.Enums;
using UnityEngine;
using Zenject;

namespace Model.Systems.Unit.Movement
{
    public sealed class LoopedMoveSystem : IEcsRunSystem
    {
        [Inject(Id = CameraEnum.Orthographic)] private Camera _camera;
        private readonly EcsFilter<Position> _transformFilter = null;
        
        public void Run()
        {
            foreach (var i in _transformFilter)
            {
                ref var position = ref _transformFilter.Get1(i);
                var nextPosition = _camera.WorldToViewportPoint(position.Value) ;
                nextPosition.y = Mathf.Repeat(nextPosition.y, 1);
                nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
                var point = _camera.ViewportToWorldPoint(new Vector3(nextPosition.x, nextPosition.y, 0));
                position.Value = point;
            }
        }
    }
}