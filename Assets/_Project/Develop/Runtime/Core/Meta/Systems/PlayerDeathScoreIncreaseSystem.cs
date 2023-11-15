using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Meta.Components;
using _Project.Develop.Runtime.Core.Services.Meta;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Meta.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class PlayerDeathScoreIncreaseSystem : ISystem
    {
        private readonly IScore _scoreCounter;
        private Filter _filter;
        private Stash<GameOverEvent> _gameOverPool;

        public PlayerDeathScoreIncreaseSystem(IScore score)
        {
            _scoreCounter = score;
        }
        
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<GameOverEvent>().Build();
            _gameOverPool = World.GetStash<GameOverEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var gameOverEvent = ref _gameOverPool.Get(entity);
                switch (gameOverEvent.Team)
                {
                    case TeamEnum.Red:
                        _scoreCounter.IncreaseBlue();
                        break;
                    case TeamEnum.Blue:
                        _scoreCounter.IncreaseRed();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}