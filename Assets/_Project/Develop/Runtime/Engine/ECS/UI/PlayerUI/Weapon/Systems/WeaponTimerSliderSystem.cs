using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Core.Weapon.Components;
using _Project.Develop.Runtime.Engine.Common.Components;
using _Project.Develop.Runtime.Engine.UI.Weapon;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.ECS.UI.PlayerUI.Weapon.Systems
{
    public class WeaponTimerSliderSystem : ISystem
    {
        private Filter _weaponFilter;
        private Filter _sliderFilter;
        private Stash<Owner> _ownerPool;
        private Stash<WeaponType> _weaponTypePool;
        private Stash<UnityComponent<WeaponsSliders>> _weaponsSlidersPool;
        private Stash<Timer<TimerBetweenShots>> _timerPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _weaponFilter = World.Filter.With<WeaponType>().With<Timer<TimerBetweenShots>>().With<Owner>().Build();
            _sliderFilter = World.Filter.With<UnityComponent<WeaponsSliders>>().With<Owner>().Build();
            _ownerPool = World.GetStash<Owner>();
            _weaponTypePool = World.GetStash<WeaponType>();
            _weaponsSlidersPool = World.GetStash<UnityComponent<WeaponsSliders>>();
            _timerPool = World.GetStash<Timer<TimerBetweenShots>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var weaponEntity in _weaponFilter)
            {
                ref var weaponOwner = ref _ownerPool.Get(weaponEntity);
                
                foreach (var sliderEntity in _sliderFilter)
                {
                    ref var sliderOwner = ref _ownerPool.Get(sliderEntity);
                    
                    if (IsAliveAndEquals(weaponOwner, sliderOwner) == false) continue;

                    ref var timer = ref _timerPool.Get(weaponEntity);
                    ref var weaponsSliders = ref _weaponsSlidersPool.Get(sliderEntity);
                    ref var weaponType = ref _weaponTypePool.Get(weaponEntity);
                    switch (weaponType.Value)
                    {
                        case WeaponEnum.Primary:
                            weaponsSliders.Value.SetPrimarySliderData(timer.TimeLeftSec, timer.InitialTimeSec);
                            break;
                        case WeaponEnum.Secondary:
                            weaponsSliders.Value.SetSecondarySliderData(timer.TimeLeftSec, timer.InitialTimeSec);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private static bool IsAliveAndEquals(Owner weaponOwner, Owner sliderOwner)
        {
            if (weaponOwner.Entity.IsNullOrDisposed() || sliderOwner.Entity.IsNullOrDisposed())
            {
                return false;
            }

            return sliderOwner.Entity.Equals(weaponOwner.Entity);
        }

        public void Dispose()
        {
        }
    }
}