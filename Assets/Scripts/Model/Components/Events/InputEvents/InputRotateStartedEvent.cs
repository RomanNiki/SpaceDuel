using Model.Enums;

namespace Model.Components.Events.InputEvents
{
    public struct InputRotateStartedEvent
    {
        public TeamEnum PlayerNumber;
        public float Axis;
    }
}