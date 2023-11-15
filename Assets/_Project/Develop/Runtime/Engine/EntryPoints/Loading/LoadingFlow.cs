using System;
using _Project.Develop.Runtime.Engine.Common;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Loading
{
    public class LoadingFlow : IStartable
    {
        public async void Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            SceneManager.LoadSceneAsync(Scenes.Meta);
        }
    }
}