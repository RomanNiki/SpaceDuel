using System;
using Core.Common.Enums;

namespace Core.Input.Components
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
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