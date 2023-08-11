using Core.Movement.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Extensions
{
    public static class WorldExtensions
    {
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
        
        public static void AddComponentToEntity<T>(this World world, in Entity entity, in T component)
            where T : struct, IComponent
        {
            world.GetStash<T>().Add(entity) = component;
        }   
        public static void AddComponentToEntity<T>(this World world, in Entity entity)
            where T : struct, IComponent
        {
            world.GetStash<T>().Add(entity);
        }
    }
}