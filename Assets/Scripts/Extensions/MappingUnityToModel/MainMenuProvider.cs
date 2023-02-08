using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Extensions.MappingUnityToModel
{
    public class MainMenuProvider : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _exitButton;

        [Inject] private EcsWorld _world;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);
            _optionsButton.onClick.AddListener(OnOptionsButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
            _optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnStartGameButtonClick()
        {
            _world.SendMessage(new StartGameRequest());
        }

        private void OnExitButtonClick()
        {
            _world.SendMessage(new CloseAppRequest());
        }
        
        private void OnOptionsButtonClick()
        {
            _world.SendMessage(new OpenOptionsRequest());
        }
    }
}