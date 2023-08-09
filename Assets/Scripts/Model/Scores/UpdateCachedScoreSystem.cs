using System;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Enums;
using Model.Extensions;
using Model.Scores.Components;
using Model.Unit.Input.Components;

namespace Model.Scores
{
    public sealed class UpdateCachedScoreSystem : IEcsRunSystem
    {
        private readonly PlayersScore _playersScore;
        private readonly EcsFilter<Team, Score, ViewUpdateRequest> _updateScoreFilter;

        public UpdateCachedScoreSystem(PlayersScore playersScore)
        {
            _playersScore = playersScore;
        }
        
        public void Run()
        {
            foreach (var i in _updateScoreFilter)
            {
                ref var team = ref _updateScoreFilter.Get1(i);
                ref var score = ref _updateScoreFilter.Get2(i);
               
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