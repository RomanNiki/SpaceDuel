using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Menu
{
    public class Menu : MonoBehaviour
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

        protected virtual void OnStartButtonClick()
        {
            StartButtonClick?.Invoke();
        }
    }
}