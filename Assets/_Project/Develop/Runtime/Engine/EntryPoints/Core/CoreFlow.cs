using System;
using _Project.Develop.Runtime.Core.Services;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Core
{
    public class CoreFlow : IStartable, IDisposable
    {
        private readonly IGame _game;

        public CoreFlow(IGame game)
        {
            _game = game;
        }

        public void Start()
        {
            _game.Start();
        }
        
        public void Dispose()
        {
            _game.Stop();
        }
    }
}