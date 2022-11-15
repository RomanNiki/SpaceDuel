using System;
using UnityEngine;

namespace Components.Player.MoveComponents
{
    [Serializable]
    public struct Move
    {
        public IViewObject ViewObject;

        public Vector3 LookDir => Quaternion.Euler(0f, 0f, ViewObject.Rotation) * Vector2.down;
        public Vector2 LastVelocity;
    }
}