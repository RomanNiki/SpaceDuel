using UnityEngine;

namespace Engine.Services.Movement.Strategies
{
    public class RigidBodyMoveStrategy : IMoveStrategy
    {
        private readonly Rigidbody _rigidBody;

        public RigidBodyMoveStrategy(Rigidbody rigidbody)
        {
            _rigidBody = rigidbody;
        }

        public void MoveTo(Vector3 position) =>
            _rigidBody.MovePosition(position);
    }
}