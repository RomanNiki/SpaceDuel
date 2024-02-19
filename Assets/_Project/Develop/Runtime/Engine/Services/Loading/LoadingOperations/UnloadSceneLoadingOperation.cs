using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations
{
    public class UnloadSceneLoadingOperation : ILoadingOperation
    {
        private readonly Scene _currentScene;

        public UnloadSceneLoadingOperation(Scene scene)
        {
            _currentScene = scene;
        }

        public string Description => "Preparing...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            await SceneManager.UnloadSceneAsync(_currentScene);
            onProgress?.Invoke(1f);
        }
    }
}