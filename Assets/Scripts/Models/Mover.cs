using UnityEngine;

namespace Models
{
    public abstract class Mover
    {
        private readonly Transformable _transformable;
        private readonly Camera _camera;

        public Mover(Transformable transformable, Camera camera)
        {
            _transformable = transformable;
            _camera = camera;
        }
        
        protected void Move()
        {
            var nextPosition = _camera.WorldToViewportPoint(_transformable.Position) ;
            nextPosition.y = Mathf.Repeat(nextPosition.y, 1);
            nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
            var point = _camera.ViewportToWorldPoint(new Vector3(nextPosition.x, nextPosition.y, 0));
            _transformable.Position = point;
        }

        protected abstract void Rotate(float direction, float deltaTime);
    }
}