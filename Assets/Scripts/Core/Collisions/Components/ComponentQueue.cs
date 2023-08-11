using System.Collections.Generic;
using Scellecs.Morpeh;

namespace Core.Collisions.Components
{
    public struct ComponentQueue<T> : IComponent
    where T : struct, IComponent
    {
        public Queue<T> Values;
    }
}