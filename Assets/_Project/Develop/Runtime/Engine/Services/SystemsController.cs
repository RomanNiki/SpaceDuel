using _Project.Develop.Runtime.Core.Services;
using Scellecs.Morpeh.Addons.Feature.Unity;

namespace _Project.Develop.Runtime.Engine.Services
{
    public class SystemsController : ISystemsController
    {
        private readonly BaseFeaturesInstaller _baseFeaturesInstaller;
        
        public SystemsController(BaseFeaturesInstaller baseFeaturesInstaller)
        {
            _baseFeaturesInstaller = baseFeaturesInstaller;
        }
        
        public void EnableSystems()
        {
            if (_baseFeaturesInstaller != null)
            {
                _baseFeaturesInstaller.gameObject.SetActive(true);
            }
        }

        public void DisableSystems()
        {
            if (_baseFeaturesInstaller != null)
            {
                _baseFeaturesInstaller.gameObject.SetActive(false);
            }
        }
    }
}