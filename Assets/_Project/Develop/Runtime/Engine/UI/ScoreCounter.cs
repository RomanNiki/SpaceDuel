using _Project.Develop.Runtime.Core.Services.Meta;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _blueScore;
        [SerializeField] private TextMeshProUGUI _redScore;

        private IScore _score;

        public void UnSubscribe()
        {
            if (_score == null) return;
            _score.BlueScoreChanged -= UpdateBlueScore;
            _score.RedScoreChanged -= UpdateRedScore;
        }

        public void Subscribe(IScore score)
        {
            _score = score;
            _blueScore.text = score.BlueScore.ToString();
            _redScore.text = score.RedScore.ToString();
            score.BlueScoreChanged += UpdateBlueScore;
            score.RedScoreChanged += UpdateRedScore;
        }

        private void UpdateRedScore(int score) => _redScore.text = score.ToString();

        private void UpdateBlueScore(int score) => _blueScore.text = score.ToString();
    }
}