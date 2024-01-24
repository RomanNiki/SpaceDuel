using System;
using System.Threading;
using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Common.Messages;
using _Project.Develop.Runtime.Engine.Infrastructure;
using _Project.Develop.Runtime.Engine.Infrastructure.Signals;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.ApplicationLifecycle.EntryPoints.Bootstrap
{
    public class BootstrapFlow : IAsyncStartable, IDisposable
    {
        private readonly IDisposable _subscriptions;

        public BootstrapFlow(ISubscriber<QuitApplicationMessage> subscriber)
        {
            var subHandles = new DisposableGroup();
            subHandles.Add(subscriber.Subscribe(QuitGame));
            _subscriptions = subHandles;
        }

        private static void QuitGame(QuitApplicationMessage obj)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await SceneManager.LoadSceneAsync(Scenes.Loading);
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}