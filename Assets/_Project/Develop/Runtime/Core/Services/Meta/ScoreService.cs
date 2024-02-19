using System;

namespace _Project.Develop.Runtime.Core.Services.Meta
{
    public class ScoreService : IScore
    {
        public int BlueScore { get; private set; }
        public int RedScore { get; private set; }
        public event Action<int> BlueScoreChanged;
        public event Action<int> RedScoreChanged;

        public void IncreaseBlue()
        {
            BlueScore++;
            BlueScoreChanged?.Invoke(BlueScore);
        }

        public void IncreaseRed()
        {
            RedScore++;
            RedScoreChanged?.Invoke(RedScore);
        }
        
        public void Reset()
        {
            BlueScore = 0;
            RedScore = 0;
            BlueScoreChanged?.Invoke(BlueScore);
            RedScoreChanged?.Invoke(RedScore);
        }
    }
}