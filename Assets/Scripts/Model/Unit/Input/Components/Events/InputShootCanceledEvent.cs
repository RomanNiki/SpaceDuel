using Model.Enums;

namespace Model.Unit.Input.Components.Events
{
    public struct InputShootCanceledEvent
    {
        public TeamEnum PlayerTeamEnum;
        public WeaponEnum Weapon;
    }
}