using Models.Player;

namespace Events.InputEvents
{
    public struct InputRotateStartedEvent
    {
        public Team PlayerNumber;
        public float Axis;
    }
}