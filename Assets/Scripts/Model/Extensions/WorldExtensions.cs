using Leopotam.Ecs;

namespace Model.Extensions
{
    public static class WorldExtensions
    {
        public static void SendMessage<T>(this EcsWorld world, in T messageEvent)
            where T : struct
        {
            world.NewEntity().Get<T>() = messageEvent;
        }
    }
}