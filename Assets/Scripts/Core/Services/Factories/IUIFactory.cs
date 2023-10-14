using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Core.Services.Factories
{
    public interface IUIFactory
    {
        UniTask OpenGameplayHud();
        UniTask OpenControlsWindow(Action startGameAction);
        void CloseGameplayHud();
        void CloseControlsWindow();
    }
}