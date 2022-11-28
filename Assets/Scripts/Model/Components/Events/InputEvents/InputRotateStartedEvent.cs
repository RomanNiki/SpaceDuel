using Model.Enums;

namespace Components.Events.InputEvents
{
    public struct InputRotateStartedEvent
    {
        public TeamEnum PlayerNumber;
        public float Axis;
    }
}