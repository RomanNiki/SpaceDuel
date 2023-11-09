using System;
using Core.Common.Enums;
using Core.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.UI.Controls
{
    public class ControlsWindow : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        private Action _onStart;
        private IInput _input;
        
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

        private void OnEnable() => _startButton.onClick.AddListener(OnStartButtonClick);

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _input.StartPrimaryShot -= OnStartClick;
            _input.StartSecondaryShot -= OnStartClick;
            _onStart = null;
        }
    }
}