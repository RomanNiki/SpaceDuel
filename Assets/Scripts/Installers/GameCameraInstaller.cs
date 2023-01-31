using Cinemachine;
using Model.Enums;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Installers
{
    public class GameCameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _orthoCamera;
        [SerializeField] private CinemachineImpulseSource _impulseSource;
        
        private void OnValidate()
        {
            if (_orthoCamera)
                if (_orthoCamera.orthographic == false)
                {
                    Assert.IsTrue(_orthoCamera.orthographic, $"{nameof(_orthoCamera)} can't be not orthographic");
                    _orthoCamera = null;
                }
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().WithId(CameraEnum.Orthographic).FromInstance(_orthoCamera).AsSingle();
            Container.Bind<CinemachineImpulseSource>().FromInstance(_impulseSource).AsSingle();
        }
    }
}