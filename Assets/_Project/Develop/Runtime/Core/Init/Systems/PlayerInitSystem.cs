using _Project.Develop.Runtime.Core.Common;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Init.Components;
using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Player.Components;
using _Project.Develop.Runtime.Core.Views.Components;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Init.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class PlayerInitSystem : IInitializer
    {
        public World World { get; set; }

        public void OnAwake()
        {
            var positionPool = World.GetStash<Position>();
            var teamPool = World.GetStash<Team>();
            var spawnPointFilter = World.Filter.With<SpawnPointTag>().With<Position>().With<Team>().Build();
            foreach (var entity in spawnPointFilter)
            {
                ref var position = ref positionPool.Get(entity);
                ref var team = ref teamPool.Get(entity);
                var objectId = team.Value == TeamEnum.Red ? ObjectId.RedPlayer : ObjectId.BluePlayer;
                var player = CreatePlayer(position.Value, objectId);
                CreateUI(team.Value, position.Value, new Owner { Entity = player });
            }
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