using Core.Enums;
using Scellecs.Morpeh;

namespace Core.Input.Components
{
    public struct InputShootCanceledEvent : IComponent
    {
        public TeamEnum PlayerTeamEnum;
        public WeaponEnum Weapon;
    }
}