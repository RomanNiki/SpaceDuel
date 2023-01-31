using Model.Enums;

namespace Model.Unit.Input.Components.Events
{
    public struct InputRotateStartedEvent
    {
        public TeamEnum PlayerNumber;
        public float Axis;
    }
}