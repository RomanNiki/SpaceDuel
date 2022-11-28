using Model.Enums;

namespace Model.Components.Events.InputEvents
{
    public struct InputShootCanceledEvent
    {
        public TeamEnum PlayerTeamEnum;
        public WeaponEnum Weapon;
    }
}