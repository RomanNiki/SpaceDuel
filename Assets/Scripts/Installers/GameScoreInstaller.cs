using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Enums;
using Model.Extensions;
using Model.Scores.Components;
using Model.Unit.Input.Components;
using TMPro;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameScoreInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        [Inject] private EcsWorld _world;
        [Inject] private PlayersScore _score;

        public override void InstallBindings()
        {
            CreateScore(TeamEnum.Blue, _settings.BlueTeamScoreText);
            CreateScore(TeamEnum.Red, _settings.RedTeamScoreText);
        }

        private void CreateScore(TeamEnum team, TMP_Text scoreText)
        {
            var score = _world.NewEntity();
            score.Get<Score>().Value = GetScore(team);;
            score.Get<Team>().Value = team;
            score.Get<UnityComponent<TMP_Text>>().Value = scoreText;
            score.Get<ViewUpdateRequest>();
        }

        private int GetScore(TeamEnum team)
        {
            var scoreValue = team switch
            {
                TeamEnum.Blue => _score.BlueScore,
                TeamEnum.Red => _score.RedScore,
                _ => 0
            };

            return scoreValue;
        }

        [Serializable]
        public class Settings
        {
            public TMP_Text BlueTeamScoreText;
            public TMP_Text RedTeamScoreText;
        }
    }
}