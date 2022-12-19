using UnityEngine;

namespace Model.Components.Unit.MoveComponents
{
    public struct Rotation
    {
        public float Value;
        public Vector3 LookDir => Quaternion.Euler(0f, 0f, Value) * Vector2.down;
    }
}