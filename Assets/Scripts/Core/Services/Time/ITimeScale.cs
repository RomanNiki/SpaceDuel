﻿using Cysharp.Threading.Tasks;

namespace Core.Services.Time
{
    public interface ITimeScale
    {
        float TimeScale { get; }
        UniTask SlowDown(float target, float duration = 3f);
        UniTask Accelerate(float target, float duration = 3f);
    }
}