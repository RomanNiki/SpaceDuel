using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Extensions;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents.Input;
using Model.Enums;
using Zenject;

namespace Model.Systems
{
    public sealed class UpdateCachedScoreSystem : IEcsRunSystem
    {
        [Inject] private PlayersScore _playersScore;
        private readonly EcsFilter<Team, Score, ViewUpdateRequest> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var team = ref _filter.Get1(i);
                ref var score = ref _filter.Get2(i);

                switch (team.Value)
                {
                    case TeamEnum.Blue:
                        _playersScore.BlueScore = score.Value;
                        break;
                    case TeamEnum.Red:
                        _playersScore.RedScore = score.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}