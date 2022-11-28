using UnityEngine;

namespace Model.Components.Unit.MoveComponents
{
    public struct TransformData
    {
        public Vector2 Position;
        public float Rotation;
        public Vector3 LookDir => Quaternion.Euler(0f, 0f, Rotation) * Vector2.down;
    }
}