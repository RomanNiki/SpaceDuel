using Model.Enums;

namespace Model.Unit.Input.Components.Events
{
    public struct InputRotateStartedEvent
    {
        public TeamEnum PlayerTeam;
        public float Axis;
    }
}