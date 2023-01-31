using System;
using Model.Buffs;
using Model.Unit.EnergySystems;
using Model.Unit.Movement;
using UnityEngine;
using Views;
using Views.Systems;
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
            public RestartGameSystem.Settings Restart;
            public SunBuffEntityExecuteSystem.Settings Buff;
            public PrepareGameSystem.Settings PrepairGame;
        }
        
        [Serializable]
        public class PlayerSettings
        {
            public PlayerForceSystem.Settings Move;
            public PlayerRotateSystem.Settings Rotate;
            public SunChargeSystem.Settings SolarCharger;
        }

        public override void InstallBindings()
        {
            Application.targetFrameRate = _targetFrameRate;
            Container.BindInstance(_player.Move).IfNotBound();
            Container.BindInstance(_player.Rotate).IfNotBound();
            Container.BindInstance(_player.SolarCharger).IfNotBound();
            Container.BindInstance(_gameInstaller.Restart).IfNotBound();
            Container.BindInstance(_gameInstaller.Buff).IfNotBound();
            Container.BindInstance(_gameInstaller.PrepairGame).IfNotBound();
        }
    }
}