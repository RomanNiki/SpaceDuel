using System;
using Models.Player;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Space Duel/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
       // public EnemySpawner.Settings EnemySpawner;
       // public GameRestartHandler.Settings GameRestartHandler;
       // public GameInstaller.Settings GameInstaller;
        [SerializeField] private PlayerSettings _player;
        public PlayerSettings Player => _player;
        

        [Serializable]
        public class PlayerSettings
        {
            public PlayerMover.Settings PlayerMoveHandler;
            /*public PlayerShootHandler.Settings PlayerShootHandler;
            public PlayerDamageHandler.Settings PlayerCollisionHandler;
            public PlayerHealthWatcher.Settings PlayerHealthWatcher;*/
        }
        
        public override void InstallBindings()
        {
            // Use IfNotBound to allow overriding for eg. from play mode tests
            /*Container.BindInstance(EnemySpawner).IfNotBound();
            Container.BindInstance(GameRestartHandler).IfNotBound();
            Container.BindInstance(GameInstaller).IfNotBound();*/

            Container.BindInstance(_player.PlayerMoveHandler).IfNotBound();
            /*Container.BindInstance(_player.PlayerShootHandler).IfNotBound();
            Container.BindInstance(_player.PlayerCollisionHandler).IfNotBound();
            Container.BindInstance(_player.PlayerHealthWatcher).IfNotBound();*/
        }
    }
}