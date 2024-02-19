using System;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Core.Services.Factories
{
    public interface IUIFactory
    {
        UniTask OpenGameplayHud();
        UniTask OpenPauseMenu();
        UniTask OpenControlsWindow(Action startGameAction);
        void CloseGameplayHud();
        void CloseControlsWindow();
        void ClosePauseMenu();
    }
}