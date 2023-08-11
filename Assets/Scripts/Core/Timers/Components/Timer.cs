using Scellecs.Morpeh;

namespace Core.Timers.Components
{
    public struct Timer<TTimerFlag> : IComponent
    where TTimerFlag : struct, IComponent
    {
        public float TimeLeftSec;
    }
}