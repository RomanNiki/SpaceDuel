using System;
using Leopotam.Ecs;
using Model.Components.Requests;

namespace Views.UI.Menu
{
    public class OptionsSystem : IEcsRunSystem
    {
        private EcsFilter<OpenOptionsRequest> _filter;

        public void Run()
        {
            if (_filter.IsEmpty())
                return;
            throw new NotImplementedException();
        }
    }
}