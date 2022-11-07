using System;
using Models;
using Models.Player;
using Models.Player.Weapon;
using Models.Player.Weapon.Bullets;
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

        }
        [Serializable]
        public class PlayerSettings
        {
            public PlayerModel.Settings PlayerModel;
            public PlayerMover.Settings PlayerMover;
            public DamageHandler.Settings DamageHandler;
            public SolarCharger.Settings SolarCharger;
            public DefaultGun.Settings BulletGun;
            public DefaultGun.Settings MinGun;
            public DamagerModel.Settings Bullet;
        }

        public override void InstallBindings()
        {
            Application.targetFrameRate = _targetFrameRate;
            Container.BindInstance(_player.PlayerModel).IfNotBound();
            Container.BindInstance(_player.PlayerMover).IfNotBound();
            Container.BindInstance(_player.DamageHandler).IfNotBound();
            Container.BindInstance(_player.SolarCharger).IfNotBound();
            Container.BindInstance(_player.BulletGun).IfNotBound();
            Container.BindInstance(_player.MinGun).IfNotBound();
            Container.BindInstance(_player.Bullet).IfNotBound();
            Container.BindInstance(GameInstaller.GameInstaller).IfNotBound();
            Container.BindInstance(GameInstaller.RestartGameHandler).IfNotBound();
        }
    }
}