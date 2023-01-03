using Controller.EntityToGameObject;
using Extensions.MappingUnityToModel;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using Model.Components.Weapons;
using UnityEngine;

namespace Views.Systems
{
    internal sealed class GunSoundSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnityComponent<GunAudioUnityComponent>, ShootIsPossible, ShotMadeEvent, UnityComponent<AudioClip>> _gunsMadeShot = null;

        public void Run()
        {
            foreach (var i in _gunsMadeShot)
            {
                var audioUnityComponent = _gunsMadeShot.Get1(i).Value;
                audioUnityComponent.PlayShoot(_gunsMadeShot.Get4(i).Value);
            }
        }
    }
}