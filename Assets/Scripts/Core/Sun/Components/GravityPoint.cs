using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Sun.Components
{
    [Serializable]
    public struct GravityPoint : IComponent
    {
        [Range(1, 100)] public float OuterRadius;
        [Range(0.5f, 10)] public float InnerRadius;
    }
}