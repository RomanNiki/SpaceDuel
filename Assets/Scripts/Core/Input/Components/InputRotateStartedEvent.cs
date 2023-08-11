using Core.Enums;
using Scellecs.Morpeh;

namespace Core.Input.Components
{
    public struct InputRotateStartedEvent : IComponent
    {
        public TeamEnum PlayerTeam;
        public float Axis;
    }
}