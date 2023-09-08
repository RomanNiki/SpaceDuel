using UnityEngine;

namespace Core.Extensions
{
    public static class MathExtensions
    {
        public static float CalculateRotationFromVelocity(Vector2 velocity)
        {
            return Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        }
    }
}