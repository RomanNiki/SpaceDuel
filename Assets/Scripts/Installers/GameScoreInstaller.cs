using Models;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameScoreInstaller : MonoInstaller
    {
        [SerializeField] private GameScore.Settings _settings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameScore>().AsSingle()
                .WithArguments(_settings);
        }
    }
}