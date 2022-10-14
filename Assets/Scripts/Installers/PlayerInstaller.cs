using System;
using Models.Player;
using Player.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        
        public override void InstallBindings()
        {
            var input = new PlayerInput();
            Container.Bind<InputActionMap>().FromInstance(_settings.Team == Team.Blue ? input.Player : input.Player1 ).AsSingle();
            Container.Bind<PlayerModel>().AsSingle()
                .WithArguments(_settings.MaxHealth, _settings.Rigidbody, _settings.MeshRenderer);
            Container.Bind<PlayerShooter>().AsSingle().WithArguments(new DefaultGun(), new DefaultGun());
            Container.Bind<PlayerInputRouter>().FromNew().AsSingle();
            Container.BindInterfacesTo<PlayerMover>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public float MaxHealth;
            public Rigidbody2D Rigidbody;
            public MeshRenderer MeshRenderer;
            public Team Team;
        }
    }
}