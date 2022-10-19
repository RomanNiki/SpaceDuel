using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Installers
{
    public class PlayerOrthoCameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _orthoCamera;
        
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
            Container.Bind<Camera>().FromInstance(_orthoCamera).AsSingle();
        }
    }
}