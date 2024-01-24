using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Menu
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [FormerlySerializedAs("_pauseButton")] [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameObject _settingsPanelRoot;
        
        public event Action StartButtonClick;
        public event Action ExitButtonClick;

        private void Start()
        {
            _settingsPanelRoot.SetActive(false);
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _optionsButton.onClick.AddListener(OnPauseButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _optionsButton.onClick.RemoveListener(OnPauseButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnPauseButtonClick()
        {
            _settingsPanelRoot.SetActive(!_settingsPanelRoot.activeSelf);
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