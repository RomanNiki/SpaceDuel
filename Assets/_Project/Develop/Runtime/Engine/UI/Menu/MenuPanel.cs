using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Engine.Common.Messages;
using _Project.Develop.Runtime.Engine.Infrastructure.Signals;
using _Project.Develop.Runtime.Engine.Services.AssetLoaders;
using _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace _Project.Develop.Runtime.Engine.UI.Menu
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _endButtonButton;
        [SerializeField] private GameObject _settingsPanelRoot;
        [SerializeField] private GameObject _quitPanelRoot;
        private LoadingScreenAssetLoader _loadingScreenAssetLoader;
        private IAssets _assets;
        private readonly Queue<ILoadingOperation> _loadingOperations = new();
        private bool _isStarting;
        private IPublisher<QuitApplicationMessage> _quitPublisher;
        public event Action StartButtonClick;
        public event Action OptionsButtonClick;
        public event Action EndButtonClick;

        [Inject]
        public void Construct(LoadingScreenAssetLoader loadingScreenAssetLoader, IAssets assets, IPublisher<QuitApplicationMessage> quitPublisher)
        {
            _loadingScreenAssetLoader = loadingScreenAssetLoader;
            _assets = assets;
            _quitPublisher = quitPublisher;
        }

        private void CreateLoadingQueue()
        {
            _loadingOperations.Clear();
            _loadingOperations.Enqueue(new LoadGameAssetsLoadingOperation(_assets));
            _loadingOperations.Enqueue(new UnloadSceneLoadingOperation(SceneManager.GetActiveScene()));
            _loadingOperations.Enqueue(new LoadGameLoadingOperation());
        }

        private void Awake()
        {
#if UNITY_WEBGL
            _endButtonButton.gameObject.SetActive(false);
#endif
            DisablePanels();
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _optionsButton.onClick.AddListener(OnOptionsButtonClick);
            _endButtonButton.onClick.AddListener(OnEndButtonClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            _endButtonButton.onClick.RemoveListener(OnEndButtonClick);
        }

        private void DisablePanels()
        {
            _settingsPanelRoot.SetActive(false);
            _quitPanelRoot.SetActive(false);
        }

        private void OnStartButtonClick()
        {
            if (_isStarting)
            {
                return;
            }

            CreateLoadingQueue();
            _loadingScreenAssetLoader.LoadAndDestroy(_loadingOperations).Forget();
            _isStarting = true;
            StartButtonClick?.Invoke();
        }

        private void OnOptionsButtonClick()
        {
            _settingsPanelRoot.SetActive(!_settingsPanelRoot.activeSelf);
            _quitPanelRoot.SetActive(false);
            OptionsButtonClick?.Invoke();
        }

        private void OnEndButtonClick()
        {
            _quitPanelRoot.SetActive(!_quitPanelRoot.activeSelf);
            _settingsPanelRoot.SetActive(false);
            _quitPublisher.Publish(new QuitApplicationMessage());
            EndButtonClick?.Invoke();
        }
    }
}