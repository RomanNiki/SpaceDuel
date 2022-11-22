using UnityEngine;

namespace Components.Unit.MoveComponents
{
    public struct Move
    {
        public Vector2 Acceleration;
        public Vector2 Velocity;
        public Vector2 Position;
        public float Rotation;
        public Vector3 LookDir => Quaternion.Euler(0f, 0f, Rotation) * Vector2.down;
    }
}