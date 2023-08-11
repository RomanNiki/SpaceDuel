using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Movement.Components
{
    public struct Rotation : IComponent
    {
        public float Value;
        public Vector3 LookDir => Quaternion.Euler(0f, 0f, Value) * Vector3.up;
    }
}