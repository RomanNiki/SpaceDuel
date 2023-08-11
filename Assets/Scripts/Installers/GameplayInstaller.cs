using System;
using Core.Extensions.Pause;
using Core.Extensions.Pause.Services;
using Core.Movement;
using Scellecs.Morpeh;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera _orthographicCamera;

        private void OnValidate()
        {
            if (_orthographicCamera.orthographic == false)
            {
                throw new ArgumentException("Need install orthographic camera");
            }
        }

        public override void InstallBindings()
        {
            var world = World.Create();
            Container.Bind<World>().FromInstance(world).AsSingle();
            Container.Bind<IMoveLoopService>().To<MoveLoopService>().AsSingle().WithArguments(_orthographicCamera);
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
        }
    }
}