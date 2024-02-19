using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Movement.Strategies
{
    public class TranslateMoveStrategy : IMoveStrategy
    {
        private readonly Transform _transform;

        public TranslateMoveStrategy(Transform transform)
        {
            _transform = transform;
        }

        public void MoveTo(Vector3 position)
        {
            _transform.position = position;
        }
    }
}