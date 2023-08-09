
using System;
using Cysharp.Threading.Tasks;
using Model;
using UnityEngine.SceneManagement;

namespace Extensions.Loading.LoadingOperations
{
    public class LoadMenuLoadingOperation : ILoadingOperation
    {
        public string Description { get; set; } = "Load menu...";
        
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync(Scenes.Menu, 
                LoadSceneMode.Single);
            while (loadOp.isDone == false)
            {
                await UniTask.Delay(1);
            }
            onProgress?.Invoke(1f);
        }
        
    }
}