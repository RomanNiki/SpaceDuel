using UnityEngine;

namespace Views.Moveable
{
    public class RigidBodyMoveStrategy : IMoveStrategy
    {
        private readonly Rigidbody _rigidBody;

        public RigidBodyMoveStrategy(Rigidbody rigidbody)
        {
            _rigidBody = rigidbody;
        }

        public void MoveTo(Vector2 position)
        {
            _rigidBody.MovePosition(position);
        }
    }
}