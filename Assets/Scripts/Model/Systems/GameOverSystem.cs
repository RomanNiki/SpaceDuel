using Leopotam.Ecs;
using Model.Components.Extensions;
using Model.Components.Requests;
using Model.Components.Tags;

namespace Model.Systems
{
    public class GameOverSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<PlayerTag, EntityDestroyRequest> _filter;

        public void Run()
        {
            if (_filter.GetEntitiesCount() > 0)
            {
                _world.SendMessage(new GameRestartRequest());
            }
        }
    }
}