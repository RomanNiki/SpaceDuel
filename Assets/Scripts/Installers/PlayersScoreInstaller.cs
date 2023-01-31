using Model.Extensions;
using Zenject;

namespace Installers
{
    public class PlayersScoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayersScore>().AsSingle();
        }
    }
}
