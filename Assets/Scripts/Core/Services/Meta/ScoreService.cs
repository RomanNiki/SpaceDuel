using System;

namespace Core.Services.Meta
{
    public class ScoreService : IScore
    {
        public int BlueScore { get; private set; }
        public int RedScore { get; private set; }
        public event Action BlueScoreChanged;
        public event Action RedScoreChanged;

        public void IncreaseBlue()
        {
            BlueScore++;
            BlueScoreChanged?.Invoke();
        }

        public void IncreaseRed()
        {
            RedScore++;
            RedScoreChanged?.Invoke();
        }
        
        public void Reset()
        {
            BlueScore = 0;
            RedScore = 0;
            BlueScoreChanged?.Invoke();
            RedScoreChanged?.Invoke();
        }
    }
}