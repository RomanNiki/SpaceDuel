using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Input;

namespace _Project.Develop.Runtime.Engine.UI.Controls
{
    public class ControlsController : IDisposable
    {
        private readonly IInput _input;
        private readonly ControlsWindowView _windowView;
        private readonly Action _onStart;

        public ControlsController(Action startGameAction, IInput input, ControlsWindowView windowView)
        {
            _input = input;
            _windowView = windowView;
            _onStart = startGameAction;
            input.StartPrimaryShot += OnShotClick;
            input.StartSecondaryShot += OnShotClick;
            windowView.StartButtonClick += OnStart;
        }

        public void Dispose()
        {
            _input.StartPrimaryShot -= OnShotClick;
            _input.StartSecondaryShot -= OnShotClick;
            _windowView.StartButtonClick -= OnStart;
        }
        
        private void OnShotClick(TeamEnum teamEnum) => OnStart();
        private void OnStart() => _onStart?.Invoke();
    }
}