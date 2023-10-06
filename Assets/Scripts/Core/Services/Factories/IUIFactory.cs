using System;
using System.Threading.Tasks;

namespace Core.Services.Factories
{
    public interface IUIFactory
    {
        Task OpenGameplayHud();
        Task OpenControlsWindow(Action startGameAction);
        void CloseGameplayHud();
        void CloseControlsWindow();
    }
}