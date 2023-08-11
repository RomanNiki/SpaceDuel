using Scellecs.Morpeh;
using UnityEngine;

namespace Views.Components
{
    public struct UnityComponent<T> : IComponent
        where T : Object
    {
        public UnityComponent(T component)
        {
            Value = component;
        }

        public T Value;
    }
}