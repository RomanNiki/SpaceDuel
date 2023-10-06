using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.UI
{
    public class ControlsWindow : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        private Action _onStart;

        public void Constuct(Action startGameAction) => _onStart = startGameAction;

        private void OnStartButtonClick() => _onStart?.Invoke();

        private void OnEnable() => _startButton.onClick.AddListener(OnStartButtonClick);

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _onStart = null;
        }
    }
}