using System.Collections.Generic;
using Extensions.AssetLoaders;
using Extensions.Loading.LoadingOperations;
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
        private LoadingScreenProvider _provider;
        private GameAssetsLoadProvider _assetsLoadProvider;

        [Inject]
        public void Constructor(LoadingScreenProvider provider, GameAssetsLoadProvider assetsLoadProvider)
        {
            _provider = provider;
            _assetsLoadProvider = assetsLoadProvider;
        }
        
        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private async void OnStartGameButtonClick()
        {
            Queue<ILoadingOperation> loadingOperations = new();
            loadingOperations.Enqueue(new LoadGameAssets(_assetsLoadProvider));
            loadingOperations.Enqueue(new LoadGameLoadingOperation());
            await _provider.LoadAndDestroy(loadingOperations);
        }

        private static void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}