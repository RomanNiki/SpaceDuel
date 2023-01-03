using Leopotam.Ecs;
using Model.Components.Requests;
using UnityEngine;

namespace Views.UI.Menu
{
    public class CloseGameSystem : IEcsRunSystem
    {
        private EcsFilter<CloseAppRequest> _filter;

        public void Run()
        {
            if (_filter.IsEmpty())
                return;
            
            Application.Quit();
        }
    }
}