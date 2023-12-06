using _Project.Develop.Runtime.Engine.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Editor
{
    [InitializeOnLoad]
    public class AutoRunScene : MonoBehaviour
    {
        static AutoRunScene()
        {
            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        private static void PlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.EnteredPlayMode) return;
            var sceneIndex = Scenes.Bootstrap;

            if (SceneManager.GetActiveScene().buildIndex != sceneIndex)
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}