using Model.Enums;

namespace Model.Components.Events.InputEvents
{
    public struct InputShootStartedEvent
    {
        public TeamEnum PlayerTeamEnum;
        public WeaponEnum Weapon;
    }
}