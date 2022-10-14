using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Zenject;

namespace Installers
{
    public class InputManagerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputManager _inputManager;
        [SerializeField] private Camera _orthoCamera;
        
        private void OnValidate()
        {
            if (_orthoCamera)
                if (_orthoCamera.orthographic == false)
                {
                    Assert.IsTrue(_orthoCamera.orthographic, "_orthoCamera can't be not orthographic");
                    _orthoCamera = null;
                }
        }
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerInputManager>().FromInstance(_inputManager).AsSingle();
            Container.Bind<Camera>().FromInstance(_orthoCamera).AsSingle();
        }
    }
}