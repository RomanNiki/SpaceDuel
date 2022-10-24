using System;
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
        // public EnemySpawner.Settings EnemySpawner;
        // public GameRestartHandler.Settings GameRestartHandler;
        [SerializeField] private int _targetFrameRate = 200;
        [SerializeField] private GameInstaller.Settings _gameInstaller;
        [SerializeField] private PlayerSettings _player;

        public PlayerSettings Player => _player;
        public GameInstaller.Settings GameInstaller => _gameInstaller;


        [Serializable]
        public class PlayerSettings
        {
            public PlayerModel.Settings PlayerModel;
            public PlayerMover.Settings PlayerMover;
            public DamageHandler.Settings DamageHandler;
            public DefaultGun.Settings BulletGun;
            public DefaultGun.Settings MinGun;
            public DamagerModel.Settings Bullet;
        }

        public override void InstallBindings()
        {
            // Use IfNotBound to allow overriding for eg. from play mode tests
            /*Container.BindInstance(EnemySpawner).IfNotBound();
            Container.BindInstance(GameRestartHandler).IfNotBound();
            Container.BindInstance(GameInstaller).IfNotBound();*/
            Application.targetFrameRate = _targetFrameRate;
            Container.BindInstance(_player.PlayerModel).IfNotBound();
            Container.BindInstance(_player.PlayerMover).IfNotBound();
            Container.BindInstance(_player.DamageHandler).IfNotBound();
            Container.BindInstance(_player.BulletGun).IfNotBound();
            Container.BindInstance(_player.MinGun).IfNotBound();
            Container.BindInstance(_player.Bullet).IfNotBound();
            Container.BindInstance(GameInstaller).IfNotBound();
        }
    }
}