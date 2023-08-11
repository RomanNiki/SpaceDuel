using Core.Movement;
using UnityEngine;

namespace Services
{
    public sealed class MoveLoopService : IMoveLoopService
    {
        private readonly Camera _camera;

        public MoveLoopService(Camera camera)
        {
            _camera = camera;
        }

        public Vector2 LoopPosition(Vector2 position)
        {
            var nextPosition = _camera.WorldToViewportPoint(new Vector3(position.x, position.y, 1));
            nextPosition.x = Mathf.Repeat(nextPosition.x, 1f);
            nextPosition.y = Mathf.Repeat(nextPosition.y, 1f);
            var viewportPoint = new Vector3(nextPosition.x, nextPosition.y, 1f);
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}