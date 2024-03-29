﻿using UnityEngine;

namespace _Project.Develop.Runtime.Core.Extensions
{
    public static class MathExtensions
    {
        public static float CalculateRotationFromVelocity(Vector2 velocity) =>
            Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
    }
}