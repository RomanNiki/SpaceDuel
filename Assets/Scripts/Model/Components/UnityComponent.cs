using UnityEngine;

namespace Model.Components
{
    public struct UnityComponent<T>
        where T : Object
    {
        public T Value;
    }
}