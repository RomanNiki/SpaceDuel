using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI.Menu
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        public event Action StartButtonClick;

        private void OnEnable()
        {
            _startButton.OnClickAsync();
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