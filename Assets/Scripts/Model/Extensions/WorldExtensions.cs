using Leopotam.Ecs;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Model.Extensions
{
    public static class WorldExtensions
    {
        public static void SendMessage<T>(this EcsWorld world, in T messageEvent)
            where T : struct
        {
            world.NewEntity().Get<T>() = messageEvent;
        }

        public static float ScaleValue(float min, float max, float value)
        {
            return (value - max) / (min - max);
        }

        public static float CalculateDistanceCoefficient(in Position dischargePosition, in Position sunPosition,
            in float minDistance, in float maxDistance)
        {
            var distance = Vector3.Distance(dischargePosition.Value, sunPosition.Value);
            return ScaleValue(minDistance, maxDistance, distance);
        }
    }
}