using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.EntityPool;

namespace _Project.Develop.Runtime.Core.Extensions
{
    public static class SystemExtensions
    {
        public static void SendMessage<T>(this World world, T component)
            where T : struct, IComponent
        {
            var pool = world.GetStash<T>();
            var entity =  world.GetPooledEntity();
            pool.Add(entity, component);
        }
    }
}