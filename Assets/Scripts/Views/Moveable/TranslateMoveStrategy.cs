using UnityEngine;

namespace Views.Moveable
{
    public class TranslateMoveStrategy : IMoveStrategy
    {
        private readonly Transform _transform;

        public TranslateMoveStrategy(Transform transform)
        {
            _transform = transform;
        }
        
        public void MoveTo(Vector2 position)
        {
            var translation = position - (Vector2)_transform.position;
            _transform.Translate(translation);
        }
    }
}