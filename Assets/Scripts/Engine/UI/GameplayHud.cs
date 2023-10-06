using Core.Services.Meta;
using UnityEngine;

namespace Engine.UI
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
