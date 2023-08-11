using System;
using Scellecs.Morpeh;

namespace Core.Timers.Components
{
    [Serializable]
    public struct TimerBetweenShotsSetup : IComponent
    {
        public float TimeSec;
    }
}