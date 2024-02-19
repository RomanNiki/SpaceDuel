using System;
using _Project.Develop.Runtime.Engine.Sounds.Ambient.Interfaces;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.ApplicationLifecycle.EntryPoints.Meta
{
    public class MetaFlow : IStartable, IDisposable
    {
        private readonly IAmbientSoundController _soundController;

        public MetaFlow(IAmbientSoundController soundController)
        {
            _soundController = soundController;
        }
        
        public void Start()
        {
            _soundController.PlayMenuAmbient();
        }

        public void Dispose()
        {
        }
    }
}