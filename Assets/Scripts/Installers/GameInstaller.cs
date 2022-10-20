using System;
using Presenters;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        
        public override void InstallBindings()
        {
            Container.BindFactory<float, Vector3, Vector3, BulletPresenter, BulletPresenter.Factory>()
                .FromPoolableMemoryPool<float, Vector3, Vector3, BulletPresenter, BulletPool>(poolBinder =>
                    poolBinder.WithInitialSize(20).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings.BulletPrefab).UnderTransformGroup("Bullets"));
        }


        private class BulletPool : MonoPoolableMemoryPool<float, Vector3, Vector3, IMemoryPool, BulletPresenter>
        {
        }
        
        [Serializable]
        public class Settings
        {
            public GameObject BulletPrefab;
        }
    }
}