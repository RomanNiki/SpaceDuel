using Model.Enums;

namespace Model.Unit.Input.Components.Events
{
    public struct InputShootStartedEvent
    {
        public TeamEnum PlayerTeamEnum;
        public WeaponEnum Weapon;
    }
}