using System.Collections.Generic;
using Core.Characteristics.Enums;
using Core.Characteristics.Player.Components;
using Core.Extensions;
using Core.Input.Components;
using Core.Movement.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Input.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class InputShootSystem : ISystem
    {
        private Filter _startShootFilter;
        private Filter _canceledShootFilter;
        private Filter _weaponFilter;
        private Stash<InputShootCanceledEvent> _inputShootCanceledPool;
        private Stash<InputShootStartedEvent> _inputShootStartedPool;
        private Stash<Team> _teamPool;
        private Stash<Owner> _ownerPool;
        private Stash<WeaponType> _weaponTypePool;
        private Stash<Rotation> _rotationPool;
        private HashSet<ShootData> _numberPlayersIsShooting;

        public World World { get; set; }

        public void OnAwake()
        {
            _startShootFilter = World.Filter.With<InputShootStartedEvent>().Build();
            _canceledShootFilter = World.Filter.With<InputShootCanceledEvent>().Build();
            _weaponFilter = World.Filter.With<Owner>().With<WeaponType>().Build();
            _inputShootStartedPool = World.GetStash<InputShootStartedEvent>();
            _inputShootCanceledPool = World.GetStash<InputShootCanceledEvent>();
            _teamPool = World.GetStash<Team>();
            _ownerPool = World.GetStash<Owner>();
            _weaponTypePool = World.GetStash<WeaponType>();
            _rotationPool = World.GetStash<Rotation>();
            _numberPlayersIsShooting = new HashSet<ShootData>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var startEntity in _startShootFilter)
            {
                ref var startShootEvent = ref _inputShootStartedPool.Get(startEntity);
                _numberPlayersIsShooting.Add(new ShootData()
                    { Weapon = startShootEvent.Weapon, TeamEnum = startShootEvent.PlayerTeamEnum });
            }

            foreach (var canceledEntity in _canceledShootFilter)
            {
                ref var shootCanceledEvent = ref _inputShootCanceledPool.Get(canceledEntity);
                var weapon = shootCanceledEvent.Weapon;
                var team = shootCanceledEvent.PlayerTeam;
                if (_numberPlayersIsShooting.TryGetValue(new ShootData(team, weapon), out var actualValue))
                {
                    _numberPlayersIsShooting.Remove(actualValue);
                }
            }

            foreach (var i in _numberPlayersIsShooting)
            {
                ProcessShootEvent(i.TeamEnum, i.Weapon);
            }
        }

        private void ProcessShootEvent(TeamEnum playerTeamEnum, WeaponEnum currentWeaponType)
        {
            foreach (var entity in _weaponFilter)
            {
                var owner = _ownerPool.Get(entity).Entity;
                if (owner.IsNullOrDisposed())
                    continue;

                var ownerTeam = _teamPool.Get(owner).Value;

                if (playerTeamEnum != ownerTeam)
                    continue;

                var weaponType = _weaponTypePool.Get(entity);

                if (currentWeaponType != weaponType.Value)
                    continue;

                var direction = _rotationPool.Get(owner).LookDir.normalized;
                MakeShooting(entity, direction);
            }
        }

        private void MakeShooting(Entity gun, in Vector2 direction)
        {
            var shooting = new ShootingRequest { Direction = direction, Entity = gun };
            World.SendMessage(shooting);
        }

        public void Dispose()
        {
        }
    }
}