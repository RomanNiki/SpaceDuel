using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Controls
{
    public class ControlsWindowView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        public event Action StartButtonClick;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            StartButtonClick?.Invoke();
        }
    }
}