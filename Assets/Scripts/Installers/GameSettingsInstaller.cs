using System;
using EntityToGameObject;
using Extensions.GameStateMachine.States;
using Model.Buffs;
using Model.Unit.SunEntity;
using UnityEngine;
using Zenject;

namespace Installers
{
    //[CreateAssetMenu(menuName = "Space Duel/Game Settings")]
    public sealed class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private int _targetFrameRate = 60;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private PlayerSettings _playerSettings;

        [Serializable]
        public class GameSettings
        {
            [SerializeField] private RestartGameState.Settings _restart;
            [SerializeField] private SunBuffEntityExecuteSystem.Settings _buff;
            [SerializeField] private StartGameState.Settings _startGame;

            public RestartGameState.Settings RestartSettings => _restart;
            public SunBuffEntityExecuteSystem.Settings BuffSettings => _buff;
            public StartGameState.Settings StartGameSettings => _startGame;
            
        }
        
        [Serializable]
        public class PlayerSettings
        {
            [SerializeField] private SunChargeSystem.Settings _solarCharger;
            [SerializeField] private PlayerInitSystem.Settings _playerInit;
            public SunChargeSystem.Settings SolarSettings => _solarCharger;
            public PlayerInitSystem.Settings PlayerInitSettings => _playerInit;
        }

        public override void InstallBindings()
        {
            Application.targetFrameRate = _targetFrameRate;
            Container.BindInstance(_playerSettings.SolarSettings).IfNotBound();
            Container.BindInstance(_playerSettings.PlayerInitSettings).IfNotBound();
            Container.BindInstance(_gameSettings.BuffSettings).IfNotBound();
            Container.BindInstance(_gameSettings.RestartSettings).IfNotBound();
            Container.BindInstance(_gameSettings.StartGameSettings).IfNotBound();
            Container.BindInstance(_playerSettings).IfNotBound();
            Container.BindInstance(_gameSettings).IfNotBound();
        }
    }
}