using System;
using Model.Enums;

namespace Model.Extensions
{
    [Serializable]
    public struct ShootData
    {
        public ShootData(TeamEnum teamEnum, WeaponEnum weapon)
        {
            TeamEnum = teamEnum;
            Weapon = weapon;
        }
        
        public TeamEnum TeamEnum { get; set; }
        public WeaponEnum Weapon { get; set; }
    }
}