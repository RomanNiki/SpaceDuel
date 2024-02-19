using System;
using _Project.Develop.Runtime.Engine.Common;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations
{
    public class LoadMenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Load menu...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            await SceneManager.LoadSceneAsync(Scenes.Meta, LoadSceneMode.Additive);
            var scene = SceneManager.GetSceneByBuildIndex(Scenes.Meta);
            SceneManager.SetActiveScene(scene);
            onProgress?.Invoke(1f);
        }
    }
}