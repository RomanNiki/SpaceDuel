using Core.Common;
using Core.Common.Enums;
using Core.Extensions;
using Core.Player.Components;
using Core.Views.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Init.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class PlayerInitSystem : IInitializer
    {
        private readonly PlayersSpawnPoints _spawnPoints;
        public World World { get; set; }

        public PlayerInitSystem(PlayersSpawnPoints spawnPoints)
        {
            _spawnPoints = spawnPoints;
        }

        public void OnAwake()
        {
            var redPlayer = CreatePlayer(_spawnPoints.RedPlayerSpawnPoint, ObjectId.RedPlayer);
            CreateUI(TeamEnum.Red, _spawnPoints.RedPlayerSpawnPoint, new Owner() { Entity = redPlayer });
            var bluePlayer = CreatePlayer(_spawnPoints.BluePlayerSpawnPoint, ObjectId.BluePlayer);
            CreateUI(TeamEnum.Blue, _spawnPoints.BluePlayerSpawnPoint, new Owner() { Entity = bluePlayer });
        }

        private Entity CreatePlayer(Vector2 spawnPoint, ObjectId id)
        {
            var playerTagStash = World.GetStash<PlayerTag>();
            var player = World.CreateEntity();
            playerTagStash.Add(player);
            SendSpawnRequest(player, spawnPoint, id);
            return player;
        }

        private void SendSpawnRequest(Entity player, Vector2 playerSpawnPoint, ObjectId id)
        {
            var startRotation = Quaternion.LookRotation(playerSpawnPoint, Vector3.forward).eulerAngles.z;
            World.SendMessage(new SpawnRequest(player, id, playerSpawnPoint, startRotation));
        }

        private void CreateUI(TeamEnum team, Vector2 position, Owner owner)
        {
            var entity = World.CreateEntity();
            var teamPool = World.GetStash<Team>();
            var ownerPool = World.GetStash<Owner>();
            teamPool.Set(entity, new Team { Value = team });
            ownerPool.Set(entity, owner);
            World.SendMessage(new SpawnRequest(entity, ObjectId.PlayerUI, position, 0f));
        }

        public void Dispose()
        {
        }
    }
}