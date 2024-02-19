using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Common;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Meta.Components;
using _Project.Develop.Runtime.Core.Player.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
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