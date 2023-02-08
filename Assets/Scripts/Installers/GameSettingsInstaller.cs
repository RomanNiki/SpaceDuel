using System;
using EntityToGameObject;
using Extensions.GameStateMachine.States;
using Model.Buffs;
using Model.Unit.Movement;
using Model.Unit.SunEntity;
using UnityEngine;
using Zenject;

namespace Installers
{
    //[CreateAssetMenu(menuName = "Space Duel/Game Settings")]
    public sealed class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private int _targetFrameRate = 60;
        [SerializeField] private GameSettings _gameInstaller;
        [SerializeField] private PlayerSettings _player;

        [Serializable]
        public class GameSettings
        {
            public RestartGameState.Settings Restart;
            public SunBuffEntityExecuteSystem.Settings Buff;
            public StartGameState.Settings PrepairGame;
        }
        
        [Serializable]
        public class PlayerSettings
        {
            public PlayerForceSystem.Settings Move;
            public PlayerRotateSystem.Settings Rotate;
            public SunChargeSystem.Settings SolarCharger;
            public PlayerInitSystem.Settings PlayerInit;
        }

        public override void InstallBindings()
        {
            Application.targetFrameRate = _targetFrameRate;
            Container.BindInstance(_player.Move).IfNotBound();
            Container.BindInstance(_player.Rotate).IfNotBound();
            Container.BindInstance(_player.SolarCharger).IfNotBound();
            Container.BindInstance(_player.PlayerInit).IfNotBound();
            Container.BindInstance(_gameInstaller.Restart).IfNotBound();
            Container.BindInstance(_gameInstaller.Buff).IfNotBound();
            Container.BindInstance(_gameInstaller.PrepairGame).IfNotBound();
        }
    }
}