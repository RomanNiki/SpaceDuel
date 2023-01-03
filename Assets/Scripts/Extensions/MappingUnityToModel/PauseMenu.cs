using System.Threading.Tasks;
using Leopotam.Ecs;
using Model.Components.Events.InputEvents;
using Model.Components.Extensions;
using Model.Components.Requests;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions.MappingUnityToModel
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continueButton;

        private EcsWorld _world;
        private TaskCompletionSource<bool> _loginCompletionSource;

        public void SetWorld(EcsWorld world)
        {
            _world = world;
        }
        
        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _continueButton.onClick.AddListener(OnContinueButtonClick);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
            _continueButton.onClick.RemoveListener(OnContinueButtonClick);
        }

        private void OnContinueButtonClick()
        {
            _world?.SendMessage(new InputPauseQuitEvent());
        }

        private void OnExitButtonClick()
        {
            _world?.SendMessage(new BackToMenuRequest());
        }
    }
}