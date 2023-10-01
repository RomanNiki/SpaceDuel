using Core.Characteristics.Player.Components;
using Core.Common;
using Core.Common.Enums;
using Core.Extensions;
using Core.Views.Components;
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
            var playerTagStash = World.GetStash<PlayerTag>();
            CreatePlayer(playerTagStash, _spawnPoints.RedPlayerSpawnPoint, ObjectId.RedPlayer);
            CreatePlayer(playerTagStash, _spawnPoints.BluePlayerSpawnPoint, ObjectId.BluePlayer);
        }

        private void CreatePlayer(Stash<PlayerTag> playerTagStash,Vector2 spawnPoint, ObjectId id)
        {
            var player = World.CreateEntity();
            playerTagStash.Add(player);
            SendSpawnRequest(player, spawnPoint, id);
        }

        private void SendSpawnRequest(Entity player, Vector2 playerSpawnPoint, ObjectId id)
        {
            var startRotation = Quaternion.LookRotation(playerSpawnPoint, Vector3.forward).eulerAngles.z;
            World.SendMessage(new SpawnRequest(player, id, playerSpawnPoint, startRotation));
        }

        public void Dispose()
        {
        }
    }
}