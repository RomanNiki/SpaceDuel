using Models.Pause;
using UnityEngine;
using Zenject;

namespace Models
{
    public abstract class Mover : IFixedTickable, IPauseHandler
    {
        private readonly Transformable _transformable;
        private readonly Camera _camera;
        private bool _isPause;
        private Vector2 _lastVelocity;
        
        public Mover(Transformable transformable, Camera camera)
        {
            _transformable = transformable;
            _camera = camera;
        }
        
        private void LoopedMove()
        {
            var nextPosition = _camera.WorldToViewportPoint(_transformable.Position) ;
            nextPosition.y = Mathf.Repeat(nextPosition.y, 1);
            nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
            var point = _camera.ViewportToWorldPoint(new Vector3(nextPosition.x, nextPosition.y, 0));
            _transformable.Position = point;
        }

        protected abstract void Rotate();
        protected abstract void Move();
        protected abstract bool CanMove();

        public void FixedTick()
        {
            if (_isPause)
                return;
            
            if (CanMove())
            {
                Move();
                Rotate();
            }
            
            LoopedMove();
        }

        public void SetPaused(bool isPaused)
        {
            _isPause = isPaused;
            if (isPaused)
            {
                _lastVelocity = _transformable.Velocity;
                _transformable.Velocity = Vector2.zero;
                return;
            }

            _transformable.Velocity = _lastVelocity;
        }
    }
}