using System;

namespace Core.Services.Meta
{
    public interface IScore
    {
        int BlueScore { get; }
        int RedScore { get; }
        event Action BlueScoreChanged;
        event Action RedScoreChanged;
        void IncreaseBlue();
        void IncreaseRed();
        void Reset();
    }
}