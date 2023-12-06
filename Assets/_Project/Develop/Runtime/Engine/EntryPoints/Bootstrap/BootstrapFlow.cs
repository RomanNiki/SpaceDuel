using System.Threading;
using _Project.Develop.Runtime.Engine.Common;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Bootstrap
{
    public class BootstrapFlow : IAsyncStartable
    {
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await SceneManager.LoadSceneAsync(Scenes.Loading);
        }
    }
}