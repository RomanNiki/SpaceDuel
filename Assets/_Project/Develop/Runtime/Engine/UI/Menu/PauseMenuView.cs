using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Menu
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;
        public event Action StartButtonClick;
        public event Action ExitButtonClick;
        
        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnStartButtonClick()
        {
            StartButtonClick?.Invoke();
        }

        private void OnExitButtonClick()
        {
            ExitButtonClick?.Invoke();
        }
    }
}