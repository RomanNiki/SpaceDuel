using System;

namespace _Project.Develop.Runtime.Core.Services.Meta
{
    public interface IScore
    {
        int BlueScore { get; }
        int RedScore { get; }
        event Action<int> BlueScoreChanged;
        event Action<int> RedScoreChanged;
        void IncreaseBlue();
        void IncreaseRed();
        void Reset();
    }
}