using Messages;
using Zenject;

namespace Installers
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<PlayerDiedMessage>();
            SignalBusInstaller.Install(Container);
        }
    }
}