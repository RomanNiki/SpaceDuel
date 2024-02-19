using _Project.Develop.Runtime.Core.Services.Meta;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.UI
{
    public class GameplayHud : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;

        private void OnDestroy() =>
            _scoreCounter.UnSubscribe();

        public void Construct(IScore score) =>
            _scoreCounter.Subscribe(score);
    }
}
