using System;
using Extensions;
using Extensions.AssetLoaders;
using Extensions.UI;
using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Movement.Components;
using Model.Weapons.Components;
using UnityEngine;
using Zenject;

namespace EntityToGameObject
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private GameAssetsLoadProvider _assets;
        private DiContainer _container;
        private Settings _settings;

        public PlayerInitSystem(GameAssetsLoadProvider assets, DiContainer container, Settings settings)
        {
            _assets = assets;
            _container = container;
            _settings = settings;
        }

        public void Init()
        {
            InitPlayer(_assets.FirstPlayer, _settings.FirstPlayerSpawnPoint, _settings.FirstPlayerRotation);
            InitPlayer(_assets.SecondPlayer, _settings.SecondPlayerSpawnPoint, _settings.SecondPlayerRotation);
        }

        private void InitPlayer(Component playerPrefab, Vector3 position, Vector3 rotation)
        {
            var playerTransform = _container
                .InstantiatePrefab(playerPrefab.gameObject, position, Quaternion.Euler(rotation), null)
                .transform;
            InitUI(ref playerTransform.GetProvider().Entity);
        }

        private void InitUI(ref EcsEntity owner)
        {
            if (_assets.PlayerUIBar == null)
            {
                throw new NullReferenceException("Bars cannot be null");
            }

            var playerUIBars = _container.InstantiatePrefab(_assets.PlayerUIBar).GetComponent<PlayerUIBars>();
            owner.Get<EnergyBar>().Bar = playerUIBars.EnergyBar;
            owner.Get<HealthBar>().Bar = playerUIBars.HealthBar;

            var uiEntity = _world.NewEntity();
            var transform = playerUIBars.transform;
            uiEntity.AddTransform(transform.position, transform.rotation.z);
            uiEntity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(playerUIBars.transform);
            uiEntity.Get<Follower>().Offset = playerUIBars.OffSet;
            uiEntity.Get<PlayerOwner>().Owner = owner;
            playerUIBars.transform.GetProvider().SetEntity(uiEntity);
        }

        [Serializable]
        public class Settings
        {
            public Vector3 FirstPlayerSpawnPoint;
            public Vector3 SecondPlayerSpawnPoint;
            public Vector3 FirstPlayerRotation;
            public Vector3 SecondPlayerRotation;
        }
    }
}