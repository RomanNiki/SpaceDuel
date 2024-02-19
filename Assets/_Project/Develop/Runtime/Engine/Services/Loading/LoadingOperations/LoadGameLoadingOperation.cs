using System;
using _Project.Develop.Runtime.Engine.Common;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations
{
    public class LoadGameLoadingOperation : ILoadingOperation
    {
        public string Description => "Load game...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            await LoadGame();
            onProgress?.Invoke(1f);
        }

        private static async UniTask LoadGame()
        {
            await SceneManager.LoadSceneAsync(Scenes.Core, LoadSceneMode.Additive);
            var scene = SceneManager.GetSceneByBuildIndex(Scenes.Core);
            SceneManager.SetActiveScene(scene);
        }
    }
}