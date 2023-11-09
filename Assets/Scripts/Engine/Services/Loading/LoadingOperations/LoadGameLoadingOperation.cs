using System;
using Cysharp.Threading.Tasks;
using Engine.Common;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Engine.Services.Loading.LoadingOperations
{
    public class LoadGameLoadingOperation : ILoadingOperation
    {
        private readonly LifetimeScope _currentScope;

        public LoadGameLoadingOperation(LifetimeScope currentScope)
        {
            _currentScope = currentScope;
        }

        public string Description { get; set; } = "Load game...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            await LoadGame();
            onProgress?.Invoke(1f);
        }

        private async UniTask LoadGame()
        {
            using (LifetimeScope.EnqueueParent(_currentScope))
            {
                await SceneManager.LoadSceneAsync(Scenes.Game, LoadSceneMode.Additive);
                var scene = SceneManager.GetSceneByName(Scenes.Game);
                SceneManager.SetActiveScene(scene); 
            }
        }
    }
}