using System;
using Enums;
using Models;
using Models.Player.Weapon;
using Systems.Unit;
using Systems.Unit.Movement;
using UnityEngine;
using Zenject;

namespace Installers
{
    //[CreateAssetMenu(menuName = "Space Duel/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private int _targetFrameRate = 200;
        [SerializeField] private GameSettings _gameInstaller;
        [SerializeField] private PlayerSettings _player;

        public PlayerSettings Player => _player;
        public GameSettings GameInstaller => _gameInstaller;
       

        [Serializable]
        public class GameSettings
        {
            public GameInstaller.Settings GameInstaller;
            public RestartGameHandler.Settings RestartGameHandler;
            public FrictionSystem.Settings Friction;

        }
        [Serializable]
        public class PlayerSettings
        {
            public PlayerForceSystem.Settings Move;
            public PlayerRotateSystem.Settings Rotate;
            public EnergySpendSystem.Settings Energy;
            public EnergyChargeSystem.Settings SolarCharger;
            public DefaultGun.Settings BulletGun;
            public DefaultGun.Settings MinGun;
        }

        public override void InstallBindings()
        {
            Application.targetFrameRate = _targetFrameRate;
            Container.BindInstance(_player.Move).IfNotBound();
            Container.BindInstance(_player.Rotate).IfNotBound();
            Container.BindInstance(_player.Energy).IfNotBound();
            Container.BindInstance(_player.SolarCharger).IfNotBound();
            Container.BindInstance(_player.BulletGun).WithId(WeaponEnum.Primary).IfNotBound();
            Container.BindInstance(_player.MinGun).WithId(WeaponEnum.Secondary).IfNotBound();
            Container.BindInstance(GameInstaller.GameInstaller).IfNotBound();
            Container.BindInstance(GameInstaller.RestartGameHandler).IfNotBound();
            Container.BindInstance(GameInstaller.Friction).IfNotBound();
        }
    }
}