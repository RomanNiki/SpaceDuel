using System.Collections.Generic;

namespace Model.Extensions
{
    public struct ContainerComponents<T> 
        where T : struct
    {
        public Queue<T> List => _list ??= new Queue<T>();
        private Queue<T> _list;
    }
}