using Models;
using Models.Player;

namespace Events.InputEvents
{
    public struct InputShootCanceledEvent
    {
        public Team PlayerTeam;
        public WeaponEnum Weapon;
    }
}