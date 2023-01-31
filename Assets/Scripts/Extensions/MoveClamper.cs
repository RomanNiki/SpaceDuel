using Model.Enums;
using Model.Extensions.Interfaces;
using UnityEngine;
using Zenject;

namespace Extensions
{
    public class MoveClamper : IMoveClamper
    {
        [Inject(Id = CameraEnum.Orthographic)] private Camera _camera;
        
        public Vector3 ClampPosition(Vector3 position)
        {
            var nextPosition = _camera.WorldToViewportPoint(position);
            nextPosition.y = Mathf.Repeat(nextPosition.y, 1);
            nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
            var viewportPoint = new Vector3(nextPosition.x, nextPosition.y, 0);
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}