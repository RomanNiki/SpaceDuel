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
            _score.BlueScoreChanged -= OnBlueScoreChanged;
            _score.RedScoreChanged -= OnRedScoreChanged;
        }

        public void Subscribe(IScore score)
        {
            _score = score;
            _blueScore.text = score.BlueScore.ToString();
            _redScore.text = score.RedScore.ToString();
            score.BlueScoreChanged += OnBlueScoreChanged;
            score.RedScoreChanged += OnRedScoreChanged;
        }

        private void OnRedScoreChanged(int score) => _redScore.text = score.ToString();

        private void OnBlueScoreChanged(int score) => _blueScore.text = score.ToString();
    }
}