using _Project.Develop.Runtime.Core.Meta.Components;
using _Project.Develop.Runtime.Core.Services;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Meta.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class GameOverSystem : ISystem
    {
        private readonly IGame _game;
        private Filter _filter;

        public GameOverSystem(IGame game)
        {
            _game = game;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<GameOverEvent>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_filter.IsEmpty())
                return;

            _game.Restart();
        }

        public void Dispose()
        {
        }
    }
}