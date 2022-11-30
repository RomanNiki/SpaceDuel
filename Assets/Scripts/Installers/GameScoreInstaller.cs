using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Enums;
using TMPro;
using UnityEngine;
using Zenject;
using Model.Components.Unit.MoveComponents.Input;

namespace Installers
{
    public class GameScoreInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        [Inject] private EcsWorld _world;

        public override void InstallBindings()
        {
            CreateScore(TeamEnum.Blue, _settings.BlueTeamScoreText);
            CreateScore(TeamEnum.Red, _settings.RedTeamScoreText);
        }

        private void CreateScore(TeamEnum team, TMP_Text scoreText)
        {
            var score = _world.NewEntity();
            score.Get<Score>();
            score.Get<Team>().Value = team;
            score.Get<UnityComponent<TMP_Text>>().Value = scoreText;
        }

        [Serializable]
        public class Settings
        {
            public TMP_Text BlueTeamScoreText;
            public TMP_Text RedTeamScoreText;
        }
    }
}