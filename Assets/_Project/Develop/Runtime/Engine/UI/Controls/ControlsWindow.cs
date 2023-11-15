using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Input;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Controls
{
    public class ControlsWindow : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        private IInput _input;
        private Action _onStart;

        private void OnEnable() => _startButton.onClick.AddListener(OnStartButtonClick);

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _input.StartPrimaryShot -= OnStartClick;
            _input.StartSecondaryShot -= OnStartClick;
            _onStart = null;
        }

        public void Constuct(Action startGameAction, IInput input)
        {
            _input = input;
            input.StartPrimaryShot += OnStartClick;
            input.StartSecondaryShot += OnStartClick;
            _onStart = startGameAction;
        }

        private void OnStartClick(TeamEnum teamEnum)
        {
            _onStart?.Invoke();
        }

        private void OnStartButtonClick() => _onStart?.Invoke();
    }
}