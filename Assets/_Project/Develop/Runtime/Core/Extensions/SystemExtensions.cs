using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Extensions
{
    public static class SystemExtensions
    {
        public static void SendMessage<T>(this World world, T component)
            where T : struct, IComponent
        {
            var pool = world.GetStash<T>();
            var entity = world.CreateEntity();
            pool.Add(entity, component);
        }
    }
}