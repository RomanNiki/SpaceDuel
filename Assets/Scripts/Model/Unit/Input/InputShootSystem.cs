using System.Collections.Generic;
using Leopotam.Ecs;
using Model.Enums;
using Model.Extensions;
using Model.Unit.Input.Components;
using Model.Unit.Input.Components.Events;
using Model.Unit.Movement.Components;
using Model.Weapons.Components;
using UnityEngine;

namespace Model.Unit.Input
{
    public sealed class InputShootSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<InputShootStartedEvent> _shootStartedFilter = null;
        private readonly EcsFilter<InputShootCanceledEvent> _shootCanceledFilter = null;
        private readonly EcsFilter<PlayerOwner, WeaponType> _weaponFilter = null;

        private readonly HashSet<ShootData> _numberPlayersIsShooting = new();

        protected override void Tick()
        {
            foreach (var i in _shootStartedFilter)
            {
                ref var inputShootStartEvent = ref _shootStartedFilter.Get1(i);
                var playerTeam = inputShootStartEvent.PlayerTeamEnum;
                var weaponType = inputShootStartEvent.Weapon;
                ProcessShootEvent(playerTeam, weaponType, true);
                _numberPlayersIsShooting.Add(new ShootData(playerTeam, weaponType));
            }

            foreach (var i in _shootCanceledFilter)
            {
                ref var inputShootCanceledEvent = ref _shootCanceledFilter.Get1(i);
                var playerTeam = inputShootCanceledEvent.PlayerTeamEnum;
                var weaponType = inputShootCanceledEvent.Weapon;
                ProcessShootEvent(playerTeam, weaponType, false);
                _numberPlayersIsShooting.RemoveWhere(x => x.TeamEnum == playerTeam && x.Weapon == weaponType);
            }

            foreach (var i in _numberPlayersIsShooting)
            {
                ProcessShootEvent(i.TeamEnum, i.Weapon, true);
            }
        }

        private void ProcessShootEvent(TeamEnum playerTeamEnum, WeaponEnum weaponType, bool isShoot)
        {
            foreach (var i in _weaponFilter)
            {
                ref var owner = ref _weaponFilter.Get1(i);
                if (owner.Owner.IsAlive() == false)
                {
                    continue;
                }
                ref var weapon = ref _weaponFilter.Get2(i);
                ref var ownerTeam = ref owner.Owner.Get<Team>().Value;
                
                if (playerTeamEnum != ownerTeam)
                    continue;
                if (weaponType != weapon.Type)
                    continue;

                ref var gun = ref _weaponFilter.GetEntity(i);

                var direction = owner.Owner.Get<Rotation>().LookDir;
                if (isShoot)
                {
                    MakeShooting(ref gun, direction);
                }
                else
                {
                    CancelShooting(ref gun);
                }
            }
        }

        private static void MakeShooting(ref EcsEntity gun, in Vector2 direction) =>
            gun.Get<Shooting>().Direction = direction;

        private static void CancelShooting(ref EcsEntity gun) => gun.Del<Shooting>();
    }
}