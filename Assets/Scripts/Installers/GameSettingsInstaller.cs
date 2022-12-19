using System;
using Model.Systems;
using Model.Systems.Unit;
using Model.Systems.Unit.Movement;
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
            public GameInstaller.Settings GameInstaller;
            public RestartGameSystem.Settings Restart;
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
            Container.BindInstance(_gameInstaller.GameInstaller).IfNotBound();
            Container.BindInstance(_gameInstaller.Restart).IfNotBound();
        }
    }
}