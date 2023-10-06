using Core.Characteristics.Damage.Components;
using Core.Common;
using Core.Extensions;
using Core.Meta.Components;
using Core.Player.Components;

namespace Core.Characteristics.Damage.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class PlayerDiedSystem : ISystem
    {
        private Filter _filter;
        private Filter _gameOverEventFilter;
        private Stash<Team> _teamPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<PlayerTag>().With<Team>().With<DeadTag>().Build();
            _gameOverEventFilter = World.Filter.With<GameOverEvent>().Build();
            _teamPool = World.GetStash<Team>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_gameOverEventFilter.IsEmpty() == false) return;

            foreach (var entity in _filter)
            {
                World.SendMessage(new GameOverEvent() { Team = _teamPool.Get(entity).Value });
            }
        }

        public void Dispose()
        {
        }
    }
}