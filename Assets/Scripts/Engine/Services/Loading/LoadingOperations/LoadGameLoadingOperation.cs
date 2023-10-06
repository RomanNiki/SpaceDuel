using System;
using Cysharp.Threading.Tasks;
using Engine.Common;
using UnityEngine.SceneManagement;

namespace Engine.Services.Loading.LoadingOperations
{
    public class LoadGameLoadingOperation : ILoadingOperation
    {
        public string Description { get; set; } = "Load game...";
        
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync(Scenes.Game, LoadSceneMode.Single);
            while (loadOp.isDone == false)
            {
                await UniTask.Delay(1);
            }
            onProgress?.Invoke(1f);
        }
    }
}