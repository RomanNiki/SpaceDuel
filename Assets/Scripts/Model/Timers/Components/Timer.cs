namespace Model.Timers.Components
{
    public struct Timer<TTimerFlag>
        where TTimerFlag : struct
    {
        public float TimeLeftSec;
    }
}