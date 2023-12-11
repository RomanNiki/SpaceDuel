﻿using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Views.Components;
using _Project.Develop.Runtime.Core.Views.Systems.Create;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Views
{
    public class ViewCreateFeature : UpdateFeature
    {
        private readonly IAssets _pools;

        public ViewCreateFeature(IAssets pools) => _pools = pools;

        public override void Dispose()
        {
            _pools.Cleanup();
            base.Dispose();
        }
        
        protected override void Initialize()
        {
            RegisterEvent<SpawnedEvent>();
            AddSystem(new SpawnSystem(_pools));
        }
    }
}