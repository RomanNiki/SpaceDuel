using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Extensions;
using Model.Unit.Destroy.Components.Requests;
using Model.Unit.Movement.Components.Tags;

namespace Model
{
    public sealed class GameOverSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<PlayerTag, EntityDestroyRequest> _filter;

        public void Run()
        {
            if (_filter.IsEmpty() == false)
            {
                _world.SendMessage(new GameRestartRequest());
            }
        }
    }
}