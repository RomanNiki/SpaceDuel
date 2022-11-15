using Models;
using Models.Player;

namespace Events.InputEvents
{
    public struct InputShootStartedEvent
    {
        public Team PlayerTeam;
        public WeaponEnum Weapon;
    }
}