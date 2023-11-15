using _Project.Develop.Runtime.Core.Movement;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Movement
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
            var screenPos =_camera.WorldToScreenPoint(position);

            if (screenPos.x > Screen.width)
            {
                screenPos.x = 0;
            }

            if (screenPos.x < 0)
            {
                screenPos.x = Screen.width;
            }
            
            if (screenPos.y > Screen.height)
            {
                screenPos.y = 0;
            }
            
            if (screenPos.y < 0)
            {
                screenPos.y = Screen.height;
            }
            
            return _camera.ScreenToWorldPoint(screenPos);
        }
    }
}