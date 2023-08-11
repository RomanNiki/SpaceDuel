using System.Collections.Generic;
using Core.Enums;
using Core.Input.Components;
using Core.Movement.Components;
using Core.Player.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Input.Systems
{
    public class InputShootSystem : ISystem
    {
        private Filter _startShootFilter;
        private Filter _canceledShootFilter;
        private Filter _weaponFilter;
        private Stash<InputShootCanceledEvent> _inputShootCanceledPool;
        private Stash<InputShootStartedEvent> _inputShootStartedPool;
        private Stash<Team> _teamPool;
        private Stash<PlayerOwner> _ownerPool;
        private Stash<WeaponType> _weaponTypePool;
        private Stash<Rotation> _rotationPool;
        private Stash<Shooting> _shootingPool;
        private HashSet<ShootData> _numberPlayersIsShooting;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            _startShootFilter = World.Filter.With<InputShootStartedEvent>();
            _canceledShootFilter = World.Filter.With<InputShootCanceledEvent>();
            _weaponFilter = World.Filter.With<PlayerOwner>().With<WeaponType>();
            _inputShootStartedPool = World.GetStash<InputShootStartedEvent>();
            _inputShootCanceledPool = World.GetStash<InputShootCanceledEvent>();
            _inputShootCanceledPool = World.GetStash<InputShootCanceledEvent>();
            _teamPool = World.GetStash<Team>();
            _ownerPool = World.GetStash<PlayerOwner>();
            _weaponTypePool = World.GetStash<WeaponType>();
            _rotationPool = World.GetStash<Rotation>();
            _shootingPool = World.GetStash<Shooting>();
            _numberPlayersIsShooting = new HashSet<ShootData>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var startEntity in _startShootFilter)
            {
                var startShootEvent = _inputShootStartedPool.Get(startEntity);
                ProcessShootEvent(startShootEvent.PlayerTeamEnum, startShootEvent.Weapon, true);
                _numberPlayersIsShooting.Add(new ShootData()
                    { Weapon = startShootEvent.Weapon, TeamEnum = startShootEvent.PlayerTeamEnum });
            }

            foreach (var canceledEntity in _canceledShootFilter)
            {
                var shootCanceledEvent = _inputShootCanceledPool.Get(canceledEntity);
                ProcessShootEvent(shootCanceledEvent.PlayerTeamEnum, shootCanceledEvent.Weapon, true);
                _numberPlayersIsShooting.RemoveWhere(data =>
                    data.Weapon == shootCanceledEvent.Weapon && data.TeamEnum == shootCanceledEvent.PlayerTeamEnum);
            }

            foreach (var i in _numberPlayersIsShooting)
            {
                ProcessShootEvent(i.TeamEnum, i.Weapon, true);
            }
        }

        private void ProcessShootEvent(TeamEnum playerTeamEnum, WeaponEnum currentWeaponType, bool isShoot)
        {
            foreach (var entity in _weaponFilter)
            {
                var owner = _ownerPool.Get(entity).Entity;
                if (owner == null)
                    continue;

                var ownerTeam = _teamPool.Get(owner).Value;

                if (playerTeamEnum != ownerTeam)
                    continue;

                var weaponType = _weaponTypePool.Get(entity);

                if (currentWeaponType != weaponType.Value)
                    continue;

                var direction = _rotationPool.Get(owner).LookDir;
                if (isShoot)
                {
                    MakeShooting(entity, direction);
                    continue;
                }

                CancelShooting(entity);
            }
        }

        private void MakeShooting(Entity gun, in Vector2 direction)
        {
            var shooting = new Shooting { Direction = direction };
            if (_shootingPool.Has(gun) == false)
            {
                _shootingPool.Add(gun);
            }

            _shootingPool.Get(gun) = shooting;
        }

        private void CancelShooting(Entity gun)
        {
            if (_shootingPool.Has(gun))
            {
                _shootingPool.Remove(gun);
            }
        }

        public void Dispose()
        {
        }
    }
}