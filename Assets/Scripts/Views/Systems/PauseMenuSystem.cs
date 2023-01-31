using Extensions.AssetLoaders;
using Leopotam.Ecs;
using Model.Components.Events;
using Zenject;

namespace Views.Systems
{
    public sealed class PauseMenuSystem : IEcsRunSystem
    {
        [Inject] private PauseMenuProvider _provider;
        private readonly EcsFilter<PauseEvent> _ecsFilter;
        private readonly EcsWorld _world;

        public async void Run()
        {
            if (_ecsFilter.IsEmpty()) return;
            if (_ecsFilter.Get1(0).Pause)
            {
                await _provider.Load(_world);
            }
            else
                _provider.Unload();
        }
    }
}