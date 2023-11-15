using _Project.Develop.Runtime.Engine.Common;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        public void Start()
        {
            SceneManager.LoadSceneAsync(Scenes.Loading);
        }
    }
}